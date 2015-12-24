import random

__author__ = 'ivansarno'
__version__ = 'V.1'
__doc__ = """Fuction that return a random bool"""


def flip_negative(period: int) -> bool:
    """Return False with probability 1/period."""
    x = random.randint(0, period - 1)
    return x.__bool__()


def flip_positive(period: int) -> bool:
    """Return False with probability 1/period."""
    return not random.randint(0, period - 1)
