import random

def read_dna_sequences(file_path):
    with open(file_path, 'r') as file:
        return [line.strip() for line in file.readlines()]

def print_matrix(matrix):
    """Prints the profile matrix in a readable format."""
    for nucleotide, values in sorted(matrix.items()):
        print(f"{nucleotide}: {' '.join(f'{value:.2f}' for value in values)}")

def create_profile(motifs, k):
    profile_matrix = {nucleotide: [1 / (len(motifs) * 2)] * k for nucleotide in 'ACGT'}  # Using pseudocounts
    for col in range(k):
        for motif in motifs:
            profile_matrix[motif[col]][col] += 1 / len(motifs)
    return profile_matrix

def most_probable_kmer(dna, k, profile):
    max_prob = -1
    most_prob_kmer = dna[:k]
    for i in range(len(dna) - k + 1):
        kmer = dna[i:i + k]
        prob = 1
        for j, nucleotide in enumerate(kmer):
            prob *= profile[nucleotide][j]
        if prob > max_prob:
            max_prob = prob
            most_prob_kmer = kmer
    return most_prob_kmer

def score(motifs, k):
    score = 0
    for i in range(k):
        column = [motif[i] for motif in motifs]
        most_common = max(column, key=column.count)
        score += sum(1 for base in column if base != most_common)
    return score

def gibbs_sampler(dnas, k, max_iterations=1000, verbose=True):
    motifs = [random.choice([dna[i:i + k] for i in range(len(dna) - k + 1)]) for dna in dnas]
    best_motifs = motifs[:]
    best_score = score(motifs, k)
    scores = [best_score]
    iteration = 0
    last_best_score = best_score
    no_improvement_count = 0

    while iteration < max_iterations:
        idx = random.randint(0, len(dnas) - 1)
        current_motifs = motifs[:idx] + motifs[idx+1:]
        profile = create_profile(current_motifs, k)
        probabilities = [sum(profile[base][i] for i, base in enumerate(dna[pos:pos + k])) for pos, dna in enumerate(dnas[idx])]
        chosen_idx = random.choices(range(len(dnas[idx])), weights=probabilities, k=1)[0]
        motifs[idx] = dnas[idx][chosen_idx:chosen_idx + k]
        current_score = score(motifs, k)
        scores.append(current_score)

        if current_score < best_score:
            best_motifs = motifs[:]
            best_score = current_score
            no_improvement_count = 0
        elif current_score == best_score:
            no_improvement_count += 1

        # Stop after the score remains the same for 50 consecutive iterations
        if no_improvement_count >= 50:
            break

        iteration += 1

        if verbose and iteration % 50 == 0:
            print(f"\nIteration {iteration}: Best Score: {best_score}, Average Score: {sum(scores) / len(scores)}")
            print("Profile Matrix:")
            print_matrix(profile)

    max_score = max(scores)
    avg_score = sum(scores) / len(scores)
    print(f"\nScores Summary: Best Score: {best_score}, Max Score: {max_score}, Average Score: {avg_score}")

    return best_motifs, best_score, max_score, avg_score


def find_consensus_string(motifs):
    consensus = ''
    k = len(motifs[0])
    for i in range(k):
        column = [motif[i] for motif in motifs]
        most_common = max(column, key=column.count)
        consensus += most_common
    return consensus

def print_motifs(motifs):
    print("Motifs:")
    for motif in motifs:
        print(motif)

# Path to your input file
file_path = 'input.txt'
dnas = read_dna_sequences(file_path)
k = 10  # Adjust the k-mer length as necessary

# Perform the Gibbs Sampler
best_motifs, best_score, max_score, avg_score = gibbs_sampler(dnas, k)

# Print the obtained motifs
print_motifs(best_motifs)

# Find consensus string
consensus_string = find_consensus_string(best_motifs)
print("\nConsensus String:", consensus_string)
