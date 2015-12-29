import math

__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """Abstract Genetic Algorithm"""


def standard(population, fitness, mutator, routine, change=10, selec_ratio=1.0, iterations=math.inf):
    attempts = change
    elements = len(population)
    minimum = population[-1][1]
    selections = int(selec_ratio * len(population))
    while attempts and iterations:
        for _ in range(selections):
            routine(population, fitness, mutator, elements, minimum)
        best = population[0][1]
        population.sort(key=lambda x: x[1], reverse=True)
        population = population[0:elements]
        iterations -= 1
        if population[0][1] >= best:
            attempts -= 1
        else:
            attempts = change
    return population


def dynamic(population, fitness, mutator, routine, gen_ratio, change=10, selec_ratio=1.0, iterations=math.inf,
            max_elements=math.inf, min_elements=1):
    attempts = change
    elements = len(population)
    minimum = population[-1][1]
    selections = int(selec_ratio * len(population))
    while attempts and iterations and min_elements < elements < max_elements:
        for _ in range(selections):
            routine(population, fitness, mutator, elements, minimum)
        best = population[0][1]
        population.sort(key=lambda x: x[1], reverse=True)
        population = population[0:elements]
        iterations -= 1
        elements = int(elements * gen_ratio)
        if population[0][1] >= best:
            attempts -= 1
        else:
            attempts = change
    return population
