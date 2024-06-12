# Aimen Daddi 150119879 Mert Özincegedik 150119643 
            .data                   
menu:       .asciiz "Welcome to our MIPS project!
"
					
mainu:		.asciiz		"Main menu: 
"
				
one:    	.asciiz "	1. Find Palindrome 
"
two: 		.asciiz "	2. Reverse Vowels
"
three: 		.asciiz "	3. Find Distinct Prime
"
four:    	.asciiz "	4. Lucky Number
"
five:    	.asciiz "	5. Exit
"
option:		.asciiz "Please select an option:"

error: 		.asciiz "Doesn't Work, Please select other option
"

exit:		.asciiz "Program ends. Bye :)	
"


str: .space 50 
vowels: .asciiz "aeiouAEIOU"
prompts: .asciiz "Input: "
output: .asciiz "Output: 
"




prompt: .asciiz "Enter an integer number:"
square: .asciiz "  is a square-free number

"
notsquare: .asciiz "  is not a square-free number

"

n: .asciiz "Enter the number of rows (n): "
m: .asciiz "Enter the number of columns (m): "
nowenter: .asciiz "Now you need to enter: "
afterenter: .asciiz " amount of numbers: "
newline: .asciiz "\n"
array: .space 200
bigarray: .space 200





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
				
			
			
	Q2:						
					li $v0, 4 # Pinting prompt
					la $a0, prompts
					syscall
					
									
					li $v0, 8 #read user input string
					la $a0, str
					li $a1, 50
					syscall
					
					move $t1,$a0
	
					la $a0,vowels
					move $t3,$a0
	
					#display msg3
					li $v0, 4 # sys call number for displaying a string
					la $a0, output
					syscall

					loop5:	
					lbu $t4,($t3)
					lbu $t2, ($t1)
					beqz $t2, end
					addi $t1,$t1,1
					addi $t3,$t3,1
					
					move $t5,$t2
					jal loop5
					
			end:	
					

					j main

			
	Q3:				
					li $v0, 4       # Print prompt
					la $a0, prompt
					syscall
					
					li $v0, 5       # Read integer input
					syscall
					
					move $t0, $v0   # Move input to temporary register
					
					li $t1, 2 		# specify devider which is 2
					
					
					beq $t1,$t0, isPrime	# if input is 2 then it is a square free number
					div $t0,$t1             # divide input by 2 
					mfhi $t3					#storing remainder
					mflo $t4					#storing quotient
					div $t4,$t1 				#divide again by 2 
					mfhi $t5					#storing remainder
					beq $t5,$zero, notaprime #if remainder equals zero then it is not a prime
					addi $t1,$t1,1			#counter for divider

					loop1:					#first loop that will divide input by a prime divider
						div $t4,$t1
						mfhi $t6
						beq $t6,$zero,loop2
						
						
					loop2:					#second loop if its remainder doesnt equal zero than the number is prime
						div $t4,$t1
						mflo $t7
						div $t7,$t1
						mfhi $t3
						beq $t3,$zero, notaprime
						j isPrime	
						
						
						
						
					isPrime:			#printing for square free numbers
						li $v0, 4
						la $a0, output
						syscall
						
						li $v0, 1
						move $a0, $t0
						syscall

						li $v0, 4
						la $a0, square
						syscall
						
						j exit1
						

					notaprime:			#printing for non square free numbers
						li $v0, 4
						la $a0, output
						syscall
						
						li $v0, 1
						move $a0, $t0
						syscall
						
						li $v0, 4
						la $a0, notsquare
						syscall
						
						j exit1		
						
					exit1:
						j main
		

	Q4:				
					    # Prompt user for n
					li $v0, 4
					la $a0, n
					syscall

					 # Read n
					li $v0, 5
					syscall
					move $t0, $v0

					# Prompt user for m
					li $v0, 4
					la $a0, m
					syscall

					# Read m
					li $v0, 5
					syscall
					move $t1, $v0
					mul $s0, $t1, $t0         #multiplication of n*m

					# Print nowenter
					li $v0, 4
					la $a0, nowenter
					syscall

					# Print multiplication result
					li $v0, 1
					move $a0, $s0
					syscall

					# Print afterenter
					li $v0, 4
					la $a0, afterenter
					syscall
					li $s1, 0        # s1 = 0 column counter
					li $s2, 0           # s2 = 0 number counter
					li $s3, 0       #initialize array index to 0
					la $s4, array    # s4 = array
					j savenumbers
				savenumbers:
					# read the integers one by one
					li $v0, 5
					syscall
					#store the integer and then go 4 bytes for the next number to be added
					sw $v0, ($s4)
					addi $s3, $s3, 4
					li $t5, 200
					add $s4, $s4, $s3
					addi $s2, $s2, 1    #add to number counter
					j main


				printarrayelements:
					li $t1, 0    # start index of the array
					li $t2, 200    # last element in bytes
					beq $t2, $t1, exit6  # if index equals length, end loop
					move $t0, $s4    # load saved array

					sll $t4, $t1, 2     # multiply index by 4 to get byte offset
					  add $t4, $t4, $t0   # add byte offset to base address to get address of current element
					lw $t3, ($s4)
					li $v0, 1
					move $a0, $t1
					syscall
					addi $t3, $t1, 1
					j printarrayelements
					
	
	
					exit6:
	
						j main
	
	
	
	Q5:  			li   $v0, 4    	    
					la   $a0, exit          
					syscall
					
					li   $v0, 10              # Exit
					syscall
					
					