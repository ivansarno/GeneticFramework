from bitstring import BitArray

__author__ = 'ivansarno'
__version__ = 'V.1.1'
__doc__ = """functions that combine 2 BitArray"""


def xor_cross(element1: BitArray, element2: BitArray) -> BitArray:
    """Return the xor of the elements."""
    return element1 ^ element2


def or_cross(element1: BitArray, element2: BitArray) -> BitArray:
    """Return the or of the elements."""
    return element1 | element2


def and_cross(element1: BitArray, element2: BitArray) -> BitArray:
    """Return the and of the elements."""
    return element1 & element2
