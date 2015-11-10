import random
from typing import Tuple


def random_sel(max_value: int) -> Tuple[int, int]:
    return random.randint(0, max_value - 1), random.randint(0, max_value - 1)


def nd_random_sel(max_value: int) -> Tuple[int, int]:
    first = random.randint(0, max_value - 1)
    second = first
    while first == second:
        second = random.randint(0, max_value - 1)
    return second
