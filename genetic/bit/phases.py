from typing import List, Tuple, Callable
from bitstring import BitArray

__author__ = 'ivansarno'
__version__ = 'V.1'
__doc__ = """Phases specific for algorithms on BitArray"""


def cross(couples: List[Tuple[BitArray, BitArray]], crosser: Callable[[BitArray, BitArray], BitArray]) -> list:
    """ Create a list of element from a list of couples by crossover.

    :param couples: list of couples of elements
    :param crosser: combine 2 elements
    :return: list of new elements
    """
    generation = []
    for c in couples:
        new = crosser(c[0], c[1])
        generation.append(new)
    return generation


def routine_mutation(population, selector, crosser, fitness, mutator, elements, minimum, distributor):
    # selection
    indexes = selector(elements)
    first = population[indexes[0]][0]
    second = population[indexes[1]][0]
    # cross
    new = crosser(first, second)
    # mutation
    mutator(new, distributor(len(new)))
    # fit
    value = fitness(new)
    if value > minimum:
        population.append((new, value))


def routine(population, selector, crosser, fitness, mutator, elements, minimum, extra):
    # selection
    indexes = selector(elements)
    first = population[indexes[0]][0]
    second = population[indexes[1]][0]
    # cross
    new = crosser(first, second)
    # fit
    value = fitness(new)
    if value > minimum:
        population.append((new, value))
