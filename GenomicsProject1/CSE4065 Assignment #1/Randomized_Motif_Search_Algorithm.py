import random

def read_dna_sequences(file_path):
    with open(file_path, 'r') as file:
        return [line.strip() for line in file.readlines()]

def print_motifs(motifs):
    for motif in motifs:
        print(' '.join(motif))

def print_profile(profile):
    bases = ['A', 'C', 'G', 'T']
    for base, counts in zip(bases, profile):
        print(base + ':', ' '.join(str(count) for count in counts))

def profile_most_probable_kmer(dna, k, profile):
    max_prob = -1
    most_prob_kmer = dna[:k]
    for i in range(len(dna) - k + 1):
        kmer = dna[i:i + k]
        prob = 1
        for j, nucleotide in enumerate(kmer):
            prob *= profile['ACGT'.index(nucleotide)][j]
        if prob > max_prob:
            max_prob = prob
            most_prob_kmer = kmer
    return most_prob_kmer

def score(motifs):
    consensus = ''
    for i in range(len(motifs[0])):
        counts = {'A': 0, 'C': 0, 'G': 0, 'T': 0}
        for motif in motifs:
            counts[motif[i]] += 1
        consensus += max(counts, key=counts.get)
    return sum(1 for motif in motifs for i in range(len(motif)) if motif[i] != consensus[i])

def profile_from_motifs(motifs):
    k = len(motifs[0])
    profile = [[1] * k for _ in range(4)]
    counts = {'A': 0, 'C': 1, 'G': 2, 'T': 3}
    for motif in motifs:
        for i, nucleotide in enumerate(motif):
            profile[counts[nucleotide]][i] += 1
    total = len(motifs) + 4
    profile = [[count / total for count in base_counts] for base_counts in profile]
    return profile

def randomized_motif_search(dnas, k, t):
    motifs = [dna[i:i + k] for dna in dnas for i in range(len(dna) - k + 1)]
    best_motifs = random.sample(motifs, t)
    best_score = score(best_motifs)
    score_history = [best_score]  # Track score history over iterations
    unchanged_count = 0
    iteration_count = 0
    prev_score = best_score
    while unchanged_count < 50:  # Stop after the score remains the same for 50 consecutive iterations
        iteration_count += 1
        profile = profile_from_motifs(best_motifs)
        print(f"\nIteration {iteration_count}:")
        print("Motifs:")
        print_motifs(best_motifs)
        print("Profile:")
        print_profile(profile)
        current_score = score(best_motifs)
        print(f"Score: {current_score}")
        score_history.append(current_score)
        if score_history[-50:] == [prev_score] * 50:
            break  # If the score remains the same for 50 iterations, stop the algorithm
        prev_score = current_score
        new_motifs = [profile_most_probable_kmer(dna, k, profile) for dna in dnas]
        best_motifs = new_motifs

    # At the end of the algorithm, print the motifs and find the consensus string
    print("\nFinal Motifs:")
    print_motifs(best_motifs)
    consensus_string = ''.join([max(set(col), key=col.count) for col in zip(*best_motifs)])
    print("Consensus String:", consensus_string)

# Path to your input file
file_path = 'input.txt'
dnas = read_dna_sequences(file_path)
k = 10 # Adjust the k-mer length as necessary
t = 10  # Number of motifs to initialize

# Perform Randomized Motif Search until the score remains unchanged for 5 consecutive iterations
randomized_motif_search(dnas, k, t)
