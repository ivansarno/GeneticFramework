import random
from bitstring import BitArray

__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """mutation operator for bits"""


def repl_mut(element: BitArray, index: int):
    """Replace bit at index position with random value"""
    element[index] = random.randint(0, 1)


def xor_mut(element: BitArray, index: int):
    """Replace bit at index position a xor with random value"""
    element[index] = random.randint(0, 1) ^ element[index]


def and_mut(element: BitArray, index: int):
    """Replace bit at index position a and with random value"""
    element[index] = random.randint(0, 1) & element[index]


def or_mut(element: BitArray, index: int):
    """Replace bit at index position a or with random value"""
    element[index] = random.randint(0, 1) | element[index]


def neg_mut(element: BitArray, index: int):
    """Replace bit at index position the negation"""
    element[index] = not element[index]
