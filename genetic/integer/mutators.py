import random
from typing import List

__author__ = 'ivansarno'
__version__ = 'V.1'
__doc__ = """mutation operator for integer"""


def add_mut(element: List[int], index: int, max_value: int, min_value: int = 0):
    """Sum a random integer from min_value to max_value and return sum % max_value. By default min_value = 0."""
    element[index] = (element[index] + random.randint(min_value, max_value - 1)) % max_value


def sub_mut(element: List[int], index: int, max_value: int, min_value: int = 0):
    """Subtract a random integer from min_value to max_value and return sub % max_value. By default min_value = 0"""
    element[index] = (element[index] - random.randint(min_value, max_value - 1)) % max_value


def or_mut(element: List[int], index: int, max_value: int, min_value: int = 0):
    """Bitwise or with random integer % max_value."""
    element[index] = (element[index] | random.randint(min_value, max_value - 1)) % max_value


def and_mut(element: List[int], index: int, max_value: int, min_value: int = 0):
    """Bitwise and with random integer % max_value."""
    element[index] = (element[index] & random.randint(min_value, max_value - 1)) % max_value


def xor_mut(element: List[int], index: int, max_value: int, min_value: int = 0):
    """Bitwise xor with random integer % max_value."""
    element[index] = (element[index] ^ random.randint(min_value, max_value - 1)) % max_value


def repl_mut(element: List[int], index: int, max_value: int, min_value: int = 0):
    """Replace element with a random int."""
    element[index] = random.randint(min_value, max_value)
