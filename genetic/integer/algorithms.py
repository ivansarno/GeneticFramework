import functools
import math
from genetic.generic import algorithms as gen

__author__ = 'ivansarno'
__version__ = 'V.1.0'
__doc__ = """algorithm's versions specific for integer"""


def standard(population, change, selector, distributor1, distributor2, fitness, mutator, selections, remains,
             max_value, min_value=0):
    """ Abstract Genetic Algorithm specific for integer.

    :param population: initial elements
    :param change: number of iterations without the maximum value changes
    :param selector: function that select 2 element for crossover
    :param distributor1: function to select a pivot to crossover
    :param distributor2: function to select a gene to mutation
    :param fitness: function to estimate the usefulness of an element
    :param mutator: function that applies a muntation on a gene
    :param selections: number of couples created by selection phase (number of new element is douple)
    :param remains: number of elelement preserved for new iteration
    :return: final population sorted reversed by value
    :type remains: int
    :type selections: int
    :type mutator: Callable[[object, int], type(None)]
    :type fitness: Callable[[object], int]
    :type distributor2: Callable[[int], int]
    :type distributor1: Callable[[int], int]
    :type selector: Callable[[int],Tuple[int,int]]
    :type change: int
    :type population: list
    :type max_value: int
    :type min_value: int
    :rtype: List[Tuple[object, int]]
    """
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return gen.standard(population, change, selector, distributor1, distributor2, fitness, mutator, selections,
                        remains)


def expansor(population, selector, distributor1, distributor2, fitness, mutator, ratio, max_value, min_value=0,
             iterations=math.inf, max_element=math.inf):
    """ Expands the population until a max number of elements or for a limited number of iteraration.

    :param population: initial elements
    :param selector: function that select 2 element for crossover
    :param distributor1: function to select a pivot to crossover
    :param distributor2: function to select a gene to mutation
    :param fitness: function to estimate the usefulness of an element
    :param mutator: function that applies a muntation on a gene
    :param ratio: expansion ratio
    :param iterations: number of iteration, by default unlimited
    :param max_element: max number of elements, by default unlimited
    :return: final population sorted reversed by value
    :type ratio: int
    :type iterations: int
    :type mutator: Callable[[object, int], type(None)]
    :type fitness: Callable[[object], int]
    :type distributor2: Callable[[int], int]
    :type distributor1: Callable[[int], int]
    :type selector: Callable[[int],Tuple[int,int]]
    :type max_element: int
    :type population: list
    :type max_value: int
    :type min_value: int
    :rtype: List[Tuple[object, int]]
    """
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return gen.expansor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations,
                        max_element)


def restrictor(population, selector, distributor1, distributor2, fitness, mutator, ratio, max_value, min_value=0,
               iterations=math.inf, min_element=0):
    """ Reduce the population until a min number of elements or for a limited number of iteraration.

    :param population: initial elements
    :param selector: function that select 2 element for crossover
    :param distributor1: function to select a pivot to crossover
    :param distributor2: function to select a gene to mutation
    :param fitness: function to estimate the usefulness of an element
    :param mutator: function that applies a muntation on a gene
    :param ratio: reduction ratio
    :param iterations: number of iteration, by default unlimited
    :param min_element: min mumber of elements, by default 0
    :return: final population sorted reversed by value
    :type ratio: int
    :type iterations: int
    :type mutator: Callable[[object, int], type(None)]
    :type fitness: Callable[[object], int]
    :type distributor2: Callable[[int], int]
    :type distributor1: Callable[[int], int]
    :type selector: Callable[[int],Tuple[int,int]]
    :type min_element: int
    :type population: list
    :type max_value: int
    :type min_value: int
    :rtype: List[Tuple[object, int]]
    """
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return gen.restrictor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations,
                          min_element)


def dynamic(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio,
            death_ratio, max_value, min_value=0, min_element=0, max_element=math.inf):
    """Version of the algorithm where numbers of elements selected and discarted changes dinamically.

    :param population: initial elements
    :param change: number of iterations without the maximum value changes
    :param selector: function that select 2 element for crossover
    :param distributor1: function to select a pivot to crossover
    :param distributor2: function to select a gene to mutation
    :param fitness: function to estimate the usefulness of an element
    :param mutator: function that applies a muntation on a gene
    :param selection_ratio: expansion ratio
    :param death_ratio: reduction ratio
    :param min_element: min mumber of elements, by default 0
    :param max_element: max mumber of elements, by default unlimited
    :return: final population sorted reversed by value
    :type min_element: int
    :type max_element: int
    :type mutator: Callable[[object, int], type(None)]
    :type fitness: Callable[[object], int]
    :type distributor2: Callable[[int], int]
    :type distributor1: Callable[[int], int]
    :type selector: Callable[[int],Tuple[int,int]]
    :type min_element: int
    :type death_ratio: int
    :type selection_ratio: int
    :type population: list
    :type max_value: int
    :type min_value: int
    :rtype: List[Tuple[object, int]]
    """
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return gen.dynamic(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio,
                       death_ratio, min_element, max_element)
