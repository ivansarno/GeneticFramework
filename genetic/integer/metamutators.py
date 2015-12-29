from typing import Callable, List

__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """Functions that return a complex mutator from  basic mutators, specific for integers"""

######
# type definition
Mutator = Callable[[object, int], type(None)]
IntMutator = Callable[[List[int], int], type(None)]
Distributor = Callable[[int], int]
Flip = Callable[[int], bool]
#####


def multimutator(mutator: IntMutator, distributor: Distributor) -> Mutator:
    """ Return a mutator that apply a basic mutator to multiple random genes, number of mutations is passed by
    instead of the index of a single element.

    :param mutator: basic mutator
    :param distributor: distributor that select random genes to mutate
    :return: new mutator that takes the number of elements to mutate as parameter instead of
    the index of a single element
    """

    def f(element, number, max_value, min_value):
        for _ in range(number):
            mutator(element, distributor(len(element), max_value=max_value, min_value=min_value))

    return f


def optional_mutator(mutator: IntMutator, flip: Flip, period: int) -> Mutator:
    """ Return a mutator that applies a basic mutator  with probability determined by the flip function.
    :param mutator: basic mutator
    :param flip: function thar returns a random bool
    :param period: period of flip function
    :return: new mutator that apply mutation randomly
    """

    def f(element, index, max_value, min_value):
        if flip(period):
            mutator(element, index, max_value=max_value, min_value=min_value)

    return f


def complex_mutator(mutator_list: List[IntMutator], distributor: Distributor) -> Mutator:
    """ Return a complex mutator that apply a random basic mutator from a list.
    :param mutator_list: list of basic mutator
    :param distributor: return a random index to select a mutator from mutator list
    :return: complex mutator that apply a random basic mutator from a list
    """

    def f(element, index, max_value, min_value):
        mut = mutator_list[distributor(len(mutator_list))]
        mut(element, index, max_value=max_value, min_value=min_value)
    return f


def int2gen(mutator: IntMutator, max_value: int, min_value: int=0) -> Mutator:
    """ Return a generic mutator from an integer mutator.

    :param min_value: min value to fix
    :param max_value: max value to fix
    :param mutator: basic IntMutator
    """
    def m(element, index):
        mutator(element, index, max_value=max_value, min_value=min_value)
    return m
