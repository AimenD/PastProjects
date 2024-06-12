import math
import random
import time

def read_cities(filename):
    #Reads city coordinates from a file and returns a dictionary.
    try:
        with open(filename, 'r') as f:
            cities = {}
            for line in f:
                parts = line.split()
                city_id = int(parts[0])
                x = int(parts[1])
                y = int(parts[2])
                cities[city_id] = (x, y)
        return cities
    except FileNotFoundError:
        print(f"Error: File {filename} not found.")
        return {}
    except Exception as e:
        print(f"Error reading file {filename}: {e}")
        return {}

def euclidean_distance(c1, c2):
    #Calculates the Euclidean distance between two points.
    return round(math.sqrt((c1[0] - c2[0])**2 + (c1[1] - c2[1])**2))

def initial_assignment(cities):
    #Assigns cities to two salesmen.
    city_ids = list(cities.keys())
    random.shuffle(city_ids)
    half = len(city_ids) // 2
    salesman1 = city_ids[:half]
    salesman2 = city_ids[half:]
    return salesman1, salesman2

def nearest_neighbor_tour(start_city, cities):
    #Generates a tour using the nearest neighbor heuristic.
    unvisited = set(cities.keys())
    unvisited.remove(start_city)
    tour = [start_city]
    current_city = start_city
    while unvisited:
        next_city = min(unvisited, key=lambda city: euclidean_distance(cities[current_city], cities[city]))
        tour.append(next_city)
        unvisited.remove(next_city)
        current_city = next_city
    return tour

def calculate_tour_length(tour, cities):
    #Calculates the total length of a tour.
    length = 0
    for i in range(len(tour) - 1):
        length += euclidean_distance(cities[tour[i]], cities[tour[i + 1]])
    length += euclidean_distance(cities[tour[-1]], cities[tour[0]])  # Return to start
    return length

def optimize_tours(salesman1, salesman2, cities):
    #Optimizes the tours for two salesmen.
    tour1 = nearest_neighbor_tour(salesman1[0], {k: cities[k] for k in salesman1})
    tour2 = nearest_neighbor_tour(salesman2[0], {k: cities[k] for k in salesman2})
    return tour1, tour2

def write_output(filename, total_length, tour1, tour2, length1, length2):
    #Writes the output to a file.
    with open(filename, 'w') as f:
        f.write(f"{total_length}\n")
        f.write(f"{length1} {len(tour1)}\n")
        for city in tour1:
            f.write(f"{city}\n")
        f.write("\n")
        f.write(f"{length2} {len(tour2)}\n")
        for city in tour2:
            f.write(f"{city}\n")
        f.write("\n")

def main(input_file, output_file):
    #Main function to run the TSP solver for two salesmen.
    cities = read_cities(input_file)
    if not cities:
        return
    salesman1, salesman2 = initial_assignment(cities)
    tour1, tour2 = optimize_tours(salesman1, salesman2, cities)
    length1 = calculate_tour_length(tour1, cities)
    length2 = calculate_tour_length(tour2, cities)
    total_length = length1 + length2
    write_output(output_file, total_length, tour1, tour2, length1, length2)

if __name__ == "__main__":
    input_file = "test-input-4.txt"
    output_file = "test-output-4.txt"
    start= time.time()
    main(input_file, output_file)
    end= time.time()
    print("finished in ", end-start, " seconds for ", input_file)
