import random
__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """Functions to select a pivot for crossover or an element for mutation"""


def random_dist(max_value: int) -> int:
    """Return a random index from 0 to max_value"""
    return random.randint(0, max_value - 1)

