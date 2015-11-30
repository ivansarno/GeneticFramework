from typing import List, Tuple, Callable
from bitstring import BitArray

__author__ = 'ivansarno'
__version__ = 'V.1.1'
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
