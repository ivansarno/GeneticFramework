from typing import Callable, Tuple
__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """Phases of generic genetic algorithm"""


def selection(population: list, selector:  Callable[[int], Tuple[int, int]], new: int) -> list:
    """ Return a list of couples for crossover selected randomly

    :param population: list of elements
    :param selector: funtion to select the 2 elements (return 2 indexes)
    :param new: number of copules
    :return: list of couples selected randomly
    """
    couples = []
    for _ in range(0, new):
        choice = selector(len(population))
        couples.append((population[choice[0]][0], population[choice[1]][0]))
    return couples


def cross(couples: list, distributor: Callable[[int], int]) -> list:
    """ Create a list of element from a list of couples by crossover

    :param couples:
    :param distributor: funtion that select  random pivots for crossover
    :return: list of new elements (douples of couples number)
    """
    generation = []
    for c in couples:
        pivot = distributor(len(c))
        new = c[0][0:pivot] + c[1][pivot:]
        generation.append(new)
        new = c[1][0:pivot] + c[0][pivot:]
        generation.append(new)
    return generation


def fit(population, fitness: Callable[[int], int]):
    """ Create a list of couples element * value

    :param population: list of elemet to evaluate
    :param fitness: function to estimate the value of an element
    :return: list element * value
    """
    fitted = []
    for g in population:
        fitted.append((g, fitness(g)))
    return fitted


def death(population: list, remains: int) -> list:
    """ Return a list of best elements

    :param population: original elements
    :param remains: number of element preserved
    :return: list of best element with len = remains
    """
    population.sort(key=lambda x: x[1], reverse=True)
    return population[0:remains]


def mutation(generation: list, distributor: Callable[[int], int], mutator: Callable[[object, int], type(None)]) -> list:
    """ Applay random mutation at element of the list

    :param generation: list of element
    :param distributor: function that select a random gene to mutation
    :param mutator: function that applay a mutation to a gene
    :return: list of mutated elements
    """
    for g in generation:
        mutator(g, distributor(len(g)))
    return generation
