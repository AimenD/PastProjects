module bird (
    input logic clk,
    input logic [15:0] data_in,
    output logic [15:0] data_out,
    output logic [11:0] address,
    output logic memwt
);

logic [11:0] pc = 0, ir;
logic [4:0] state = FETCH; 
logic [15:0] regbank [7:0]; 
logic zeroflag = 0; 
logic [15:0] result; 

typedef enum logic [3:0] {
    FETCH = 4'b0000,
    LDI   = 4'b0001,
    LD    = 4'b0010,
    ST    = 4'b0011,
    JZ    = 4'b0100,
    JMP   = 4'b0101,
    ALU   = 4'b0111,
    PUSH  = 4'b1000,
    POP1  = 4'b1001,
    POP2  = 4'b1100,
    CALL  = 4'b1010,
    RET1  = 4'b1011,
    RET2  = 4'b1101
} state_e;

logic zeroresult;

always_ff @(posedge clk) begin
    case(state)
        FETCH: begin
            if (data_in[15:12] == JZ)
                state <= zeroflag ? JMP : FETCH;
            else
                state <= data_in[15:12];
            ir <= data_in[11:0];
            pc <= pc + 1;
        end
        LDI: begin
            regbank[ir[2:0]] <= data_in;
            pc <= pc + 1;
            state <= FETCH;
        end
        LD: begin
            regbank[ir[2:0]] <= data_in;
            state <= FETCH;
        end
        ST: state <= FETCH;
        JMP: begin
            pc <= pc + ir;
            state <= FETCH;
        end
        ALU: begin
            regbank[ir[2:0]] <= result;
            zeroflag <= zeroresult;
            state <= FETCH;
        end
        PUSH: begin
            regbank[7] <= regbank[7] - 1;
            state <= FETCH;
        end
        POP1: begin
            regbank[7] <= regbank[7] + 1;
            state <= POP2;
        end
        POP2: begin
            regbank[ir[2:0]] <= data_in;
            state <= FETCH;
        end
        CALL: begin
            regbank[7] <= regbank[7] - 1;
            pc <= pc + ir;
            state <= FETCH;
        end
        RET1: begin
            regbank[7] <= regbank[7] + 1;
            state <= RET2;
        end
        RET2: begin
            pc <= data_in[11:0];
            state <= FETCH;
        end
    endcase
end

always_comb begin
    case (state)
        LD:    address = regbank[ir[5:3]];
        ST:    address = regbank[ir[5:3]];
        PUSH:  address = regbank[7];
        POP2:  address = regbank[7];
        CALL:  address = regbank[7];
        RET2:  address = regbank[7];
        default: address = pc;
    endcase
end

assign memwt = (state == ST) || (state == PUSH) || (state == CALL);

always_comb begin
    case (state)
        CALL: data_out = {4'b0, pc};
        default: data_out = regbank[ir[8:6]];
    endcase
end

always_comb begin
    case (ir[11:9])
        3'b000: result = regbank[ir[8:6]] + regbank[ir[5:3]];
        3'b001: result = regbank[ir[8:6]] - regbank[ir[5:3]];
        3'b010: result = regbank[ir[8:6]] & regbank[ir[5:3]];
        3'b011: result = regbank[ir[8:6]] | regbank[ir[5:3]];
        3'b100: result = regbank[ir[8:6]] ^ regbank[ir[5:3]];
        3'b111: begin
            case (ir[8:6])
                3'b000: result = ~regbank[ir[5:3]];
                3'b001: result = regbank[ir[5:3]];
                3'b010: result = regbank[ir[5:3]] + 1;
                3'b011: result = regbank[ir[5:3]] - 1;
                default: result = 16'b0;
            endcase
        end
        default: result = 16'b0;
    endcase
end

assign zeroresult = (result == 0);

endmodule
