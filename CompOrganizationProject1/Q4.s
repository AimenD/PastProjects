
		
		.data
prmpt1:     .asciiz "Input: "




		.text

main:       li   $v0, 4    	   # Syscall to print prompt string
            la   $a0, prmpt1         # li and la are pseudo instr.
            syscall
            li   $v0, 5     	   # Syscall to read an integer
            syscall
            move $s3, $v0   	   # n stored in $t0
		
		
		
		
			add $s0,$zero,$zero
			addi $t1,$s3, 1
			addi $t2, $s3, -1
			addi $t3, $s3, -2
			addi $t4, $s3, -3
			addi $t5, $s3, -4
			addi $t6, $s3, -5
			sll $t1, $t1, 2
			sll $t2, $t2, 2
			sll $t3, $t3, 2
			sll $t4, $t4, 2
			sll $t5, $t5, 2
			sll $t6, $t6, 2
			add $t7,$t2,$s2
			add $t8, $s2, $zero
			add $t9,$t2, $s2, 
			add $t10,$t3, $s2, 
			add $t11,$t4, $s2, 
			add $t12,$t5 $s2, 
			add $t13,$t6, $s2,
		mul:	lw $t14, 0($t1)
			lw $t15, 0($t2)
			lw $t16, 0($t3)
			lw $t17, 0($t4)
			lw $t18, 0($t5)
			lw $t19, 0($t6)
			mul $s4, $t14, $16
			mul $s5, $t15, $17
			mul $s6, $t18, $19
			mul $s7,$s4,$s5
			mul $s8, $s7, $s6
			
			lw 0($t1),$t20
			lw 0($t2),$t21
			lw 0($t3),$t22 
			lw 0($t4),$t23
			lw 0($t5),$t24
			lw 0($t6),$t25
			mul $s9, $t20, $22
			mul $s10, $t21, $24
			mul $s11, $t24, $25
			mul $s12,$s9,$s10
			mul $s13, $s12, $s11
			
			bne $s1,$zero,mul