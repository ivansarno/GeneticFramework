from typing import Callable

__author__ = 'ivansarno'
__version__ = 'V.1'
__doc__ = """Functions that return a complex mutator from  basic mutators"""


def multimutator(mutator: Callable[[object, int], type(None)], distributor: Callable[[int], int]) \
        -> Callable[[object, int], type(None)]:
    """ Return a mutator that apply a basic mutator to multiple random genes, number of mutations is passed by
    instead of the index of a single element.

    :param mutator: basic mutator
    :param distributor: distributor that select random genes to mutate
    :return: new mutator that takes the number of elements to mutate as parameter instead of the index of a single element
    """
    def f(element, number):
        for _ in range(number):
            mutator(element, distributor(len(element)))
    return f


def optional_mutator(mutator: Callable[[object, int], type(None)], flip: Callable[[int], bool], period: int) \
        -> Callable[[object, int], type(None)]:
    """ Return a mutator that applies a basic mutator  with probability determined by the function flip.
    :param mutator: basic mutator
    :param flip: function thar returns a random bool
    :param period: period of flip
    :return: new mutator that apply mutation randomly
    """
    def f(element, index):
        if flip(period):
            mutator(element, index)
    return f


def complex_mutator(mutator_list: list, distributor: Callable[[int], int]) -> Callable[[object, int], type(None)]:
    """ Return a complex mutator that applay a random basic mutator from a list.
    :param mutator_list: list of basic mutator
    :param distributor: return a random index to select a mutator from mutator list
    :return: complex mutator that applay a random basic mutator from a list
    """
    def f(element, index):
        mut = mutator_list[distributor(len(mutator_list))]
        mut(element, index)
    return f
