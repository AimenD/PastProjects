$LC0:
        .asciiz  "Enter your number: \000"
$LC1:
        .asciiz "%d\000"
$LC2:
        .asciiz  "Type:\000"
main:
        addiu   $sp,$sp,-160
        sw      $31,156($sp)
        sw      $fp,152($sp)
        move    $fp,$sp
        li      $2,1                        # 0x1
        sw      $2,32($fp)
        lui     $2,%hi($LC0)
        addiu   $4,$2,%lo($LC0)
        jal     printf
        nop

        addiu   $2,$fp,44
        move    $5,$2
        lui     $2,%hi($LC1)
        addiu   $4,$2,%lo($LC1)
        jal     __isoc99_scanf
        nop

        lui     $2,%hi($LC2)
        addiu   $4,$2,%lo($LC2)
        jal     printf
        nop

        addiu   $2,$fp,48
        move    $5,$2
        lui     $2,%hi($LC1)
        addiu   $4,$2,%lo($LC1)
        jal     __isoc99_scanf
        nop

        li      $2,2                        # 0x2
        sw      $2,36($fp)
        lw      $2,36($fp)
        nop
        addiu   $2,$2,-256
        sw      $2,24($fp)
        lw      $3,48($fp)
        li      $2,1                        # 0x1
        bne     $3,$2,$L2
        nop

        lw      $5,24($fp)
        lui     $2,%hi($LC1)
        addiu   $4,$2,%lo($LC1)
        jal     printf
        nop

$L2:
        lw      $3,48($fp)
        li      $2,2                        # 0x2
        bne     $3,$2,$L3
        nop

        sw      $0,28($fp)
        b       $L4
        nop

$L9:
        lw      $3,24($fp)
        li      $2,-2147483648                  # 0xffffffff80000000
        ori     $2,$2,0xf
        and     $2,$3,$2
        bgez    $2,$L5
        nop

        addiu   $2,$2,-1
        li      $3,-16                  # 0xfffffffffffffff0
        or      $2,$2,$3
        addiu   $2,$2,1
$L5:
        sw      $2,40($fp)
        lw      $2,40($fp)
        nop
        slt     $2,$2,10
        beq     $2,$0,$L6
        nop

        lw      $2,40($fp)
        nop
        andi    $2,$2,0x00ff
        addiu   $2,$2,48
        andi    $2,$2,0x00ff
        sll     $3,$2,24
        sra     $3,$3,24
        lw      $2,28($fp)
        addiu   $4,$fp,24
        addu    $2,$4,$2
        sb      $3,28($2)
        b       $L7
        nop

$L6:
        lw      $2,40($fp)
        nop
        andi    $2,$2,0x00ff
        addiu   $2,$2,55
        andi    $2,$2,0x00ff
        sll     $3,$2,24
        sra     $3,$3,24
        lw      $2,28($fp)
        addiu   $4,$fp,24
        addu    $2,$4,$2
        sb      $3,28($2)
$L7:
        lw      $2,24($fp)
        nop
        bgez    $2,$L8
        nop

        addiu   $2,$2,15
$L8:
        sra     $2,$2,4
        sw      $2,24($fp)
        lw      $2,28($fp)
        nop
        addiu   $2,$2,1
        sw      $2,28($fp)
$L4:
        lw      $2,24($fp)
        nop
        bgtz    $2,$L9
        nop

        lw      $2,28($fp)
        addiu   $3,$fp,24
        addu    $2,$3,$2
        lb      $2,28($2)
        nop
        move    $4,$2
        jal     putchar
        nop

$L3:
        move    $2,$0
        move    $sp,$fp
        lw      $31,156($sp)
        lw      $fp,152($sp)
        addiu   $sp,$sp,160
        jr      $31
        nop