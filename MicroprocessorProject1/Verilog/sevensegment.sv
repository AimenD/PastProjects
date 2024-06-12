module sevensegment (
    input logic [15:0] datain,
    output logic [6:0] display,
    output logic [3:0] grounds,
    input logic clk
);

logic [3:0] data [3:0];
logic [25:0] clk1;
logic [1:0] count;

always_ff @(posedge clk)
    clk1 <= clk1 + 1;

always_ff @(posedge clk1[15]) begin
    grounds <= {grounds[2:0], grounds[3]};
    count <= count + 1;
end

always_comb begin
    data[3] = datain[15:12];
    data[2] = datain[11:8];
    data[1] = datain[7:4];
    data[0] = datain[3:0];
end

always_comb begin
    case (data[count])
        4'h0: display = 7'b1111110;
        4'h1: display = 7'b0110000;
        4'h2: display = 7'b1101101;
        4'h3: display = 7'b1111001;
        4'h4: display = 7'b0110011;
        4'h5: display = 7'b1011011;
        4'h6: display = 7'b1011111;
        4'h7: display = 7'b1110000;
        4'h8: display = 7'b1111111;
        4'h9: display = 7'b1111011;
        4'ha: display = 7'b1110111;
        4'hb: display = 7'b0011111;
        4'hc: display = 7'b1001110;
        4'hd: display = 7'b0111101;
        4'he: display = 7'b1001111;
        4'hf: display = 7'b1000111;
        default: display = 7'b0000000; 
    endcase
end

initial begin
    count = 0;
    grounds = 4'b0100;
    clk1 = 0;
end

endmodule
