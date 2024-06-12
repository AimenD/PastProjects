module connect( input clk, input buttondata, output logic [3:0] grounds, output logic [6:0] display );




localparam    BEGINMEM=12'h000,
                  ENDMEM=12'h1ff,
                  BUTTONDATA=12'h900,
                  BUTTONCHOICE=12'h901,
                  SEVENSEG=12'hb00;

logic [15:0] memory [0:127]; 
 

logic [15:0] data_out;
logic [15:0] data_in;
logic [11:0] address;
logic memwt;


logic [15:0] ss7_out, button_in;
logic ackx;

sevensegment ss1(.datain(ss7_out),.grounds(grounds),.display(display),.clk(clk));

button_poll bt1(.clk(clk), .data_out(button_in));

bird br1 (.clk(clk),.data_in(data_in), .data_out(data_out),.address(address),.memwt(memwt));

always_comb
begin
ackx =0;
    if ( (BEGINMEM<=address) && (address<=ENDMEM) )
        begin
            data_in=memory[address];
        end
          else if ((address==BUTTONDATA)|| (address==BUTTONCHOICE))
            begin
                 ackx = 1;               
                 data_in = button_in;   
            end
    else
        begin
            data_in=16'hf345;
        end
end

always_ff @(posedge clk) 
    if (memwt)
        if ( (BEGINMEM<=address) && (address<=ENDMEM) )
            memory[address]<=data_out;
        else if ( SEVENSEG==address) 
            ss7_out<=data_out;

            
initial 
    begin
        ss7_out=0;
        $readmemh("ram.dat", memory);
    end



endmodule