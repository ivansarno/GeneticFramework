import random
from typing import Tuple
__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """Funtcions to select a random couple of element from a ordered list, returns 2 indexes"""


def random_sel(max_value: int) -> Tuple[int, int]:
    """Return 2 random indexes from 0 to max_value"""
    return random.randint(0, max_value - 1), random.randint(0, max_value - 1)


def dif_random_sel(max_value: int) -> Tuple[int, int]:
    """Return 2 different random indexes from 0 to max_value"""
    first = random.randint(0, max_value - 1)
    second = first
    while first == second:
        second = random.randint(0, max_value - 1)
    return second
