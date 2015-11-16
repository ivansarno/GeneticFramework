from typing import Callable, List, Tuple
from bitstring import BitArray

__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """Phases of genetic algorithm specific for bits seqences"""


def cross(couples: List[Tuple[BitArray, BitArray]], distributor: Callable[[int], int]) -> List[BitArray]:
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


def mutation(generation: List[BitArray], distributor: Callable[[int], int],
             mutator: Callable[[BitArray, int], BitArray]) -> List[BitArray]:
    """ Applay random mutation at element of the list

    :param generation: list of element
    :param distributor: function that select a random gene to mutation
    :param mutator: function that applay a mutation to a gene
    :return: list of mutated elements
    """
    for g in generation:
        mutator(g, distributor(len(g)))
    return generation
