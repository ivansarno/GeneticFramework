import random
__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """mutation operator for integer"""


def add_mut(element: int, max_value: int) -> int:
    """Sum a random integer from 0 to max_value and return sum % max_value."""
    return (element + random.randint(0, max_value - 1)) % max_value


def sub_mut(element: int, max_value: int) -> int:
    """Subtract a random integer from 0 to max_value and return sub % max_value."""
    return (element - random.randint(0, max_value - 1)) % max_value


def add_neg_mut(element: int, max_value: int, min_value: int) -> int:
    """Sum a random integer from min_value to max_value and return sum % max_value, min_value can be negative."""
    return (element + random.randint(min_value, max_value - 1)) % max_value


def or_mut(element: int, max_value: int) -> int:
    """Bitwise or with random integer % max_value."""
    return (element | random.randint(0, max_value - 1)) % max_value


def and_mut(element: int, max_value: int) -> int:
    """Bitwise and with random integer % max_value."""
    return (element & random.randint(0, max_value - 1)) % max_value


def xor_mut(element: int, max_value: int) -> int:
    """Bitwise xor with random integer % max_value."""
    return (element ^ random.randint(0, max_value - 1)) % max_value


def repl_mut(element: int, max_value: int, min_value: int = 0) -> int:
    """Replace element with a random int."""
    return random.randint(min_value, max_value)
