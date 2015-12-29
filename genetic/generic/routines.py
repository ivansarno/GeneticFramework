from genetic.generic import distributors, selectors

__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """Standard routines for genetic algorithms"""


def random_routine(population, fitness, mutator, elements, minimum):
    distributor = distributors.random_dist
    selector = selectors.random_sel
    # selection
    indexes = selector(elements)
    first = population[indexes[0]][0]
    second = population[indexes[1]][0]
    # cross
    pivot = distributor(elements)
    new = first[0:pivot] + second[pivot:]
    # mutation
    mutator(new, distributor(len(new)))
    # fit
    value = fitness(new)
    if value > minimum:
        population.append((new, value))
    # cross
    pivot = distributor(elements)
    new = second[0:pivot] + first[pivot:]
    # mutation
    mutator(new, distributor(len(new)))
    # fit
    value = fitness(new)
    if value > minimum:
        population.append((new, value))
