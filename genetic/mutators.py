import random


def add_mut(element: int, max_value: int) -> int:
    return (element + random.randint(0, max_value - 1)) % max_value


def sub_mut(element: int, max_value: int) -> int:
    return (element - random.randint(0, max_value - 1)) % max_value


def add_neg_mut(element: int, max_value: int, min_value: int) -> int:
    return (element + random.randint(min_value, max_value - 1)) % max_value


def or_mut(element: int, max_value: int) -> int:
    return (element | random.randint(0, max_value - 1)) % max_value


def and_mut(element: int, max_value: int) -> int:
    return (element & random.randint(0, max_value - 1)) % max_value


def xor_mut(element: int, max_value: int) -> int:
    return (element ^ random.randint(0, max_value - 1)) % max_value
