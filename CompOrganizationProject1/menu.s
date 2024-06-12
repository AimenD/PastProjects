# Aimen Daddi 150119879 / Oussama Lyazidi 150119871
            .data                   # Data memory area.
									# Used Strings
menu:       .asciiz "Welcome to our MIPS project!
"
					
mainu:			.asciiz		"Main menu: 
"
				
one:    	.asciiz "	1. Base Converter 
"
two: 		.asciiz "	2. Add Rational Number 
"
three: 		.asciiz "	3. Text Parser 
"
four:    	.asciiz "	4. Mystery Matrix Operation
"
five:    	.asciiz "	5. Exit
"
option:		.asciiz "Please select an option:"

firstNum:     .asciiz "Enter the first numerator: "
firstDeno:     .asciiz "Enter the first denominator: "
SecondNum: 	.asciiz "Enter the second numerator: "
SecondDeno: 	.asciiz "Enter the second denominator "
equal:      .asciiz "="
plus:    	.asciiz "+"
dash:		.asciiz "/"
line:		.asciiz "

"

error: 		.asciiz "Doesn't Work, Please select other option
	
"

exit:		.asciiz "Program ends. Bye :)	
"

          
			.text   

############ This Part Prints the Main menu ####################			
main:       li   $v0, 4    	    
            la   $a0, menu         								
            syscall
			
			li   $v0, 4    	   
            la   $a0, mainu         
            syscall
			
			li   $v0, 4    	   
            la   $a0, one         
            syscall
			
			li   $v0, 4    	   
            la   $a0, two         
            syscall
			
			li   $v0, 4    	   
            la   $a0, three         
            syscall
								
			li   $v0, 4    	   		
            la   $a0, four         	
            syscall					
									
			li   $v0, 4    	   									
            la   $a0, five         	
            syscall					
									
									
			li   $v0, 4    	   		
            la   $a0, option										
            syscall
			
################################################################
			
	
			#Reads Option Input
			li   $v0, 5     	   
            syscall
            move $t0, $v0   	   
			
			
			#Jumping to corresponding option
			beq $t0,1,Q1
			beq $t0,2,Q2
			beq $t0,3,Q3
			beq $t0,4,Q4
			beq $t0,5,Q5
			j main
			
			
			
			
	Q1:				li   $v0, 4    	    
					la   $a0, error          
					syscall	
					
					j main
				
			
			
	Q2:				li   $v0, 4    	    
					la   $a0, firstNum          
					syscall
					li   $v0, 5     	    
					syscall
					move $t0, $v0   	    
					
					li   $v0, 4    	    
					la   $a0, firstDeno          
					syscall
					li   $v0, 5     	    
					syscall
					move $t1, $v0   	    
					
					li   $v0, 4    	    
					la   $a0, SecondNum          
					syscall
					li   $v0, 5     	    
					syscall
					move $t2, $v0   	    
					
					li   $v0, 4    	    
					la   $a0, SecondDeno			 
					syscall
					li   $v0, 5     	    
					syscall
					move $t3, $v0   	    

					
					mult $t0,$t3
					mflo $t4
					
					mult $t1,$t3
					mflo $t5
					
					mult $t2,$t1
					mflo $t6
					
					
					add $s0,$t4,$t6
					
				
					move $a0, $t0           # Prints First Numerator 
					li   $v0, 1
					syscall
					
					li   $v0, 4	             
					la   $a0, dash
					syscall
					
					move $a0, $t1           # Prints First Denominator 
					li   $v0, 1
					syscall
					
					li   $v0, 4	             
					la   $a0, plus
					syscall
					
					move $a0, $t2           # Prints Second Numerator
					li   $v0, 1
					syscall
					
					li   $v0, 4	             
					la   $a0, dash
					syscall
					
					move $a0, $t3          # Prints Second Denominator
					li   $v0, 1
					syscall
					
					li   $v0, 4	             
					la   $a0, equal
					syscall
										
					move $a0, $s0           # Prints Numerator Result
					li   $v0, 1
					syscall
					
					li   $v0, 4	             
					la   $a0, dash
					syscall
										
					move $a0, $t5           # Prints Denominator Result
					li   $v0, 1
					syscall
					
					li   $v0, 4	             
					la   $a0, line
					syscall

					j main

			
	Q3:				li   $v0, 4    	    
					la   $a0, error          
					syscall	
					
					j main

	Q4:				li   $v0, 4    	    
					la   $a0, error          
					syscall	
					
					j main
	
	Q5:  			li   $v0, 4    	    
					la   $a0, exit          
					syscall
					
					li   $v0, 10              # Exit
					syscall
					
					