.data
    spaceshipBitmapAddr: 0x0200
    planetBitmapAddr: 0x0210
    spaceshipXAddr: 0x0220
    spaceshipYAddr: 0x0221
    planetXAddr: 0x0222
    planetYAddr: 0x0223
    initialSpaceshipX: 0x0140
    initialSpaceshipY: 0x00F0
    initialPlanetX: 0x0140
    initialPlanetY: 0x00F0
.code

MAIN
    ldi 7 0x01ff
    ldi 1 0x07f7
    st 1 ISR_Vsync
    dec 1
    st 1 ISR_secondElapsed

    ldi 0 spaceshipBitmapAddr
    ldi 1 planetBitmapAddr
    ld 0 0
    ld 1 1

    ldi 3 0x0080
    st 0 3
    ldi 3 0x07E0
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0x1FF8
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0x3FFC
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0x7FFE
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0x7FFE
    st 1 3

    inc 0
    inc 1

    ldi 3 0x03E0
    st 0 3
    ldi 3 0xFFFF
    st 1 3

    inc 0
    inc 1

    ldi 3 0x07F0
    st 0 3
    ldi 3 0xFFFF
    st 1 3

    inc 0
    inc 1

    ldi 3 0x0FF8
    st 0 3
    ldi 3 0xFFFF
    st 1 3
    
    inc 0
    inc 1

    ldi 3 0x3FFE
    st 0 3
    ldi 3 0xFFFF
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0xFFFF
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0xFFFF
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0x7FFE
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0x7FFE
    st 1 3

    inc 0
    inc 1

    ldi 3 0x03E0
    st 0 3
    ldi 3 0x3FFC
    st 1 3

    inc 0
    inc 1

    ldi 3 0x07F0
    st 0 3
    ldi 3 0x1FF8
    st 1 3

    inc 0
    inc 1

    ldi 3 0x01C0
    st 0 3
    ldi 3 0x07E0
    st 1 3

    ldi 0 spaceshipXAddr
    ld 0 0
    ldi 1 initialSpaceshipX
    st 0 1

    ldi 0 spaceshipYAddr
    ld 0 0
    ldi 1 initialSpaceshipY
    st 0 1

    ldi 0 planetXAddr
    ld 0 0
    ldi 1 initialPlanetX
    st 0 1

    ldi 0 planetYAddr
    ld 0 0
    ldi 1 initialPlanetY
    st 0 1

    sti

gameLoop
    jmp gameLoop


PROC_moveSpaceship
    mov 0 0

RET_moveSpaceship
ret

PROC_movePlanet
    push 2
    push 3
    push 4

    ldi 0 planetXAddr
    ld 1 0
    ldi 2 planetYAddr
    ld 3 2

    ldi 4 0x000A
    sub 3 3 4
    add 4 4 4
    add 1 1 4

    st 0 1
    st 2 3

    pop 4
    pop 3
    pop 2
RET_movePlanet
ret


PROC_render
    ldi 0 spaceshipXAddr
    ld 0 0
    ldi 1 initialSpaceshipX
    ld 1 1
    st 0 1

    ldi 0 spaceshipYAddr
    ld 0 0
    ldi 1 initialSpaceshipY
    ld 1 1
    st 0 1

    ldi 0 planetXAddr
    ld 0 0
    ldi 1 initialPlanetX
    ld 1 1
    st 0 1

    ldi 0 planetYAddr
    ld 0 0
    ldi 1 initialPlanetY
    ld 1 1
    st 0 1
RET_render
ret

ISR_Vsync
    push 0
    push 1

    sti
    call PROC_render
    
    pop 1
    pop 0
RET_ISR_Vsync
iret

ISR_secondElapsed
    push 0
    push 1

    sti
    call PROC_movePlanet

    pop 1
    pop 0
RET_ISR_secondElapsed
iret
