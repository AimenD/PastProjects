# Input: number of inputs, n, and n integers;   Output: Sum of integers
            .data                    # Data memory area.
prmpt1:     .asciiz "Enter the first numerator: "
prmpt2:     .asciiz "Enter the first denominator: "
prmpt3: 	.asciiz "Enter the second numerator: "
prmpt4: 	.asciiz "Enter the second denominator "
equal:      .asciiz "="
plus:    	.asciiz "+"
dash:		.asciiz "/"
          
			.text           	   # Code area starts here
main:       li   $v0, 4    	   # Syscall to print prompt string
            la   $a0, prmpt1         # li and la are pseudo instr.
            syscall
            li   $v0, 5     	   # Syscall to read an integer
            syscall
            move $t0, $v0   	   # n stored in $t0
			
			li   $v0, 4    	   # Syscall to print prompt string
            la   $a0, prmpt2         # li and la are pseudo instr.
            syscall
            li   $v0, 5     	   # Syscall to read an integer
            syscall
            move $t1, $v0   	   # n stored in $t0
			
			li   $v0, 4    	   # Syscall to print prompt string
            la   $a0, prmpt3         # li and la are pseudo instr.
            syscall
            li   $v0, 5     	   # Syscall to read an integer
            syscall
            move $t2, $v0   	   # n stored in $t0
			
			li   $v0, 4    	   # Syscall to print prompt string
            la   $a0, prmpt4			# li and la are pseudo instr.
            syscall
            li   $v0, 5     	   # Syscall to read an integer
            syscall
            move $t3, $v0   	   # n stored in $t0

			
			mult $t0,$t3
			mflo $t4
			
			mult $t1,$t3
			mflo $t5
			
			mult $t2,$t1
			mflo $t6
			

			add $s0,$t4,$t6
			
			
			
            move $a0, $t0           # Syscall to print an integer
            li   $v0, 1
            syscall
			
			li   $v0, 4	            # syscal to print string
            la   $a0, dash
            syscall
			
			move $a0, $t1           # Syscall to print an integer
            li   $v0, 1
            syscall
			
			li   $v0, 4	            # syscal to print string
            la   $a0, plus
            syscall
			
			move $a0, $t2           # Syscall to print an integer
            li   $v0, 1
            syscall
			
			li   $v0, 4	            # syscal to print string
            la   $a0, dash
            syscall
			
			
			move $a0, $t3          # Syscall to print an integer
            li   $v0, 1
            syscall
			
			li   $v0, 4	            # syscal to print string
            la   $a0, equal
            syscall
			
			
            move $a0, $s0           # Syscall to print an integer
            li   $v0, 1
            syscall
			
			
			li   $v0, 4	            # syscal to print string
            la   $a0, dash
            syscall
			
			
			move $a0, $t5           # Syscall to print an integer
            li   $v0, 1
            syscall

		
			
            li   $v0, 10              # Syscall to exit
            syscall
