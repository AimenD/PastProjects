
		.data
		
		
		
$LC0:
        .asciiz  "\012 Please Enter any String :  \000"
$LC1:
        .asciiz  "\012 Please Enter the Character that you want to Remove "
        .asciiz  ":  \000"
$LC2:
        .asciiz  "%c\000"
$LC3:
        .asciiz  "\012 The Final String after Removing All Occurrences of "
        .asciiz  "'%c' = %s \000"
		
		.text
main:
        addiu   $sp,$sp,-152
        sw      $31,148($sp)
        sw      $fp,144($sp)
        move    $fp,$sp
        lui     $2,%hi($LC0)
        addiu   $4,$2,%lo($LC0)
        jal     printf
        nop

        addiu   $2,$fp,36
        move    $4,$2
        jal     gets
        nop

        lui     $2,%hi($LC1)
        addiu   $4,$2,%lo($LC1)
        jal     printf
        nop

        addiu   $2,$fp,136
        move    $5,$2
        lui     $2,%hi($LC2)
        addiu   $4,$2,%lo($LC2)
        jal     __isoc99_scanf
        nop

        addiu   $2,$fp,36
        move    $4,$2
        jal     strlen
        nop

        sw      $2,28($fp)
        sw      $0,24($fp)
        b       $L2
        nop

$L6:
        lw      $2,24($fp)
        addiu   $3,$fp,24
        addu    $2,$3,$2
        lb      $3,12($2)
        lb      $2,136($fp)
        nop
        bne     $3,$2,$L3
        nop

        lw      $2,24($fp)
        nop
        sw      $2,32($fp)
        b       $L4
        nop

$L5:
        lw      $2,32($fp)
        nop
        addiu   $2,$2,1
        addiu   $3,$fp,24
        addu    $2,$3,$2
        lb      $3,12($2)
        lw      $2,32($fp)
        addiu   $4,$fp,24
        addu    $2,$4,$2
        sb      $3,12($2)
        lw      $2,32($fp)
        nop
        addiu   $2,$2,1
        sw      $2,32($fp)
$L4:
        lw      $3,32($fp)
        lw      $2,28($fp)
        nop
        slt     $2,$3,$2
        bne     $2,$0,$L5
        nop

        lw      $2,28($fp)
        nop
        addiu   $2,$2,-1
        sw      $2,28($fp)
        lw      $2,24($fp)
        nop
        addiu   $2,$2,-1
        sw      $2,24($fp)
$L3:
        lw      $2,24($fp)
        nop
        addiu   $2,$2,1
        sw      $2,24($fp)
$L2:
        lw      $3,24($fp)
        lw      $2,28($fp)
        nop
        slt     $2,$3,$2
        bne     $2,$0,$L6
        nop

        lb      $2,136($fp)
        addiu   $3,$fp,36
        move    $6,$3
        move    $5,$2
        lui     $2,%hi($LC3)
        addiu   $4,$2,%lo($LC3)
        jal     printf
        nop

        move    $2,$0
        move    $sp,$fp
        lw      $31,148($sp)
        lw      $fp,144($sp)
        addiu   $sp,$sp,152
        jr      $31
        nop