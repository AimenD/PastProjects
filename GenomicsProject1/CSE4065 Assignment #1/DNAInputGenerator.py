import random
lineCount = 10
DNA_length = 500
k = "AAAAAAAAAA"
# Function to generate a random DNA string of length 500
def generateRandomDnaString(DNA_length, lineCount):
    # Prepare an input file which has 10 lines, where each line contains strings of length 500.
    # Create a list of lineCount strings, each of length 500, and then write it into file.
    with open('input.txt', 'w') as f:
        for i in range(lineCount):
            f.write(''.join(random.choice('ATGC') for _ in range(DNA_length)) + '\n')

def mutateRandomPlaceWithGivenKmer(k, lineCount):
    # Ensure the length of k is 10 for the intended mutation
    if len(k) != 10:
        raise ValueError("k must be exactly 10 characters long.")
    # Read the input file Select a random position between 0 – 490 to insert the 10-mer.
    with open('input.txt', 'r') as f:
        lines = f.readlines()
    # Modify each line as per the random position and k
    for i in range(lineCount):
        position = random.randint(0, 490)
        mutated_k = mutateKmer(k)
        lines[i] = lines[i][:position] + mutated_k + lines[i][position+10:]
        print(mutated_k)
    # Write the modified lines back to the file
    with open('input.txt', 'w') as f:
        f.writelines(lines)

def mutateKmer(k):
    # Choose 4 unique positions to mutate
    positions_to_mutate = random.sample(range(len(k)), 4)
    k_list = list(k)  # Convert the string to a list for mutation
    for pos in positions_to_mutate:
        # Choose a new nucleotide different from the current one at this position
        new_nt = random.choice([nt for nt in 'ATGC' if nt != k_list[pos]])
        k_list[pos] = new_nt
    return ''.join(k_list)

# Example usage
generateRandomDnaString(DNA_length, lineCount)
mutateRandomPlaceWithGivenKmer(k, lineCount)  # Example with 'k' being a 10-mer of 'A's
