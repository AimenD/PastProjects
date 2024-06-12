module vga_sync(
    input logic clk,
    input logic write_enable,
    input logic [15:0] data_in,
    input logic [5:0] address,
    output logic [2:0] output_color,
    output logic horizontal_sync,
    output logic vertical_sync,
    output logic interrupt_vsync
);

    logic pixel_tick, is_video_on;
    logic [9:0] horizontal_counter;
    logic [9:0] vertical_counter;

    logic [15:0] ship_bitmap [0:15];
    logic [15:0] planet_bitmap [0:15];
    logic [9:0] ship_position_x;
    logic [9:0] ship_position_y;
    logic [9:0] planet_position_x;
    logic [9:0] planet_position_y;

    localparam 	HORIZONTAL_DISPLAY  = 640,
                HORIZONTAL_FRONT_PORCH  = 48,
                HORIZONTAL_BACK_PORCH  = 16,
                HORIZONTAL_FLYBACK  = 96,
                VERTICAL_DISPLAY  = 480,
                VERTICAL_TOP_PORCH  = 10,
                VERTICAL_BOTTOM_PORCH  = 33,
                VERTICAL_FLYBACK  = 2,
                LINE_END = HORIZONTAL_FRONT_PORCH + HORIZONTAL_DISPLAY + HORIZONTAL_BACK_PORCH + HORIZONTAL_FLYBACK - 1,
                PAGE_END = VERTICAL_TOP_PORCH + VERTICAL_DISPLAY + VERTICAL_BOTTOM_PORCH + VERTICAL_FLYBACK - 1;
                  
    localparam  SPRITE_SIZE = 16,
                BEGIN_SHIP_BITMAP = 6'h00,
                BEGIN_PLANET_BITMAP = 6'h10,
                ADDR_SHIP_X = 6'h20,
                ADDR_SHIP_Y = 6'h21,
                ADDR_PLANET_X = 6'h22,
                ADDR_PLANET_Y = 6'h23;

    always_ff @(posedge clk)
        pixel_tick <= ~pixel_tick;

    always_ff @(posedge clk)
    begin
        if (pixel_tick)
        begin
            if (horizontal_counter == LINE_END)
            begin
                horizontal_counter <= 0;
                
                if (vertical_counter == PAGE_END)
                    vertical_counter <= 0;
                else
                    vertical_counter <= vertical_counter + 1;
            end
            else
            begin
                horizontal_counter <= horizontal_counter + 1;
            end
        end
    end
    
    always_ff @(posedge write_enable or posedge vertical_sync)
    begin
        if(write_enable)
        begin
            if((address >= BEGIN_SHIP_BITMAP) && (address < BEGIN_PLANET_BITMAP))
                ship_bitmap[address[3:0]] <= data_in;
            else if ((address >= BEGIN_PLANET_BITMAP) && (address < ADDR_SHIP_X))
                planet_bitmap[address[3:0]] <= data_in;
            else if (address == ADDR_SHIP_X)
            begin
                ship_position_x <= data_in[9:0];
                interrupt_vsync <= 0;
            end
            else if (address == ADDR_SHIP_Y)
            begin
                ship_position_y <= data_in[9:0];
                interrupt_vsync <= 0;
            end
            else if (address == ADDR_PLANET_X)
            begin
                planet_position_x <= data_in[9:0];
                interrupt_vsync <= 0;
            end
            else if (address == ADDR_PLANET_Y)
            begin
                planet_position_y <= data_in[9:0];
                interrupt_vsync <= 0;
            end
        end
        else if(vertical_sync)
        begin
            interrupt_vsync <= 1;
        end
    end
      
    always_comb begin
        if ((horizontal_counter < HORIZONTAL_DISPLAY) && (vertical_counter < VERTICAL_DISPLAY))
        begin
            if ((horizontal_counter >= ship_position_x) && (horizontal_counter < (ship_position_x + SPRITE_SIZE))
                && (vertical_counter >= ship_position_y) && (vertical_counter < ship_position_y + SPRITE_SIZE)
                && (ship_bitmap[vertical_counter - ship_position_y][ship_position_x + SPRITE_SIZE - horizontal_counter] == 1))
            begin
                output_color = 3'b001;    //ship
            end
            else if ((horizontal_counter >= planet_position_x) && (horizontal_counter < (planet_position_x + SPRITE_SIZE))
                && (vertical_counter >= planet_position_y) && (vertical_counter < planet_position_y + SPRITE_SIZE)
                && (planet_bitmap[vertical_counter - planet_position_y][planet_position_x + SPRITE_SIZE - horizontal_counter] == 1))
            begin
                output_color = 3'b011;    //planet
            end
            else
            begin
                output_color = 3'b010;    //background
            end
        end
        else
        begin
            output_color = 3'b000;
        end
    end

    assign horizontal_sync = (horizontal_counter >= (HORIZONTAL_DISPLAY + HORIZONTAL_BACK_PORCH) && horizontal_counter <= (HORIZONTAL_FLYBACK + HORIZONTAL_DISPLAY + HORIZONTAL_BACK_PORCH - 1));
    assign vertical_sync = (vertical_counter >= (VERTICAL_DISPLAY + VERTICAL_BOTTOM_PORCH) && vertical_counter <= (VERTICAL_DISPLAY + VERTICAL_BOTTOM_PORCH + VERTICAL_FLYBACK - 1));

    initial begin
        horizontal_counter = 0;
        vertical_counter = 0;
        pixel_tick = 0;
        ship_position_x = 0;
        ship_position_y = 0;
        planet_position_x = 0;
        planet_position_y = 0;
    end

endmodule
