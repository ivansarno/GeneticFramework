import random
__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """Functions to select a pivot for crossover or an element for mutation"""


def random_dist(max_value: int) -> int:
    """Return a random index from 0 to max_value"""
    return random.randint(0, max_value - 1)


def centre_dist(max_value: int, breadth: int=3) -> int:
    """ Return a random index in the central part of the sequence.
    :param breadth: size of central part = 1/breadth of sequence length
    """
    offset = max_value // breadth if breadth % 2 else max_value // (2 * breadth)
    return random.randint(offset, max_value-offset-1)


def edge_dist(max_value: int, breadth: int=3) -> int:
    """ Return a random index in the edge of the sequence.
    :param breadth: size of the edge = 1/breadth of sequence length
    """
    offset = max_value // breadth
    index = random.randint(-offset, offset)
    return index if index >= 0 else max_value - 1 - index
