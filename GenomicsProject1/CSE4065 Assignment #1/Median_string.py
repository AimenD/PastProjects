import itertools
import time


def hamming_distance(pattern, text):
    """Calculate the Hamming distance between two strings."""
    return sum(1 for p, t in zip(pattern, text) if p != t)


def median_string(dna_list, k):
    """Find the median string of length k that minimizes total Hamming distance."""
    min_distance = float('inf')
    median = None

    # Generate all possible k-mers
    patterns = [''.join(p) for p in itertools.product('ACGT', repeat=k)]

    # Iterate over each k-mer and calculate total Hamming distance
    for pattern in patterns:
        distance = sum(
            min(hamming_distance(pattern, text[i:i + k]) for i in range(len(text) - k + 1)) for text in dna_list)
        if distance < min_distance:
            min_distance = distance
            median = pattern

    return median


if __name__ == "__main__":
    start_time = time.time()

    # Read input from the file
    with open('input.txt', 'r') as f:
        dna_list = [line.strip() for line in f.readlines()]

    k = len(dna_list[0])  # Assuming all DNA sequences are of equal length
    median = median_string(dna_list, k)

    end_time = time.time()
    elapsed_time = end_time - start_time

    print("Median String:", median)
    print("Execution Time:", elapsed_time)
