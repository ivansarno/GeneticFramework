import math

from genetic.bit import phases
from genetic.generic.phases import fit, death

__author__ = 'ivansarno'
__version__ = 'V.1'
__doc__ = """Abstract Genetic Algorithm specific for complex cross on BitArray"""


def standard(population, change, selector, crosser, fitness, selections, remains, distributor=None, mutator=None):
    """ Abstract Genetic Algorithm.

    :param population: initial elements
    :param change: number of iterations without the maximum value changes
    :param selector: function that select 2 element for crossover
    :param crosser: function to combine 2 BitArray
    :param fitness: function to estimate the usefulness of an element
    :param selections: number of couples created by selection phase (number of new element)
    :param remains: number of elelement preserved for new iteration
    :return: final population sorted reversed by value
    :type remains: int
    :type selections: int
    :type fitness: Callable[[object], int]
    :type crosser: Callable[[BitArray, BitArray], BitArray]
    :type selector: Callable[[int],Tuple[int,int]]
    :type change: int
    :type population: list
    :rtype: List[Tuple[object, int]]
    """
    attempts = change
    routine = phases.routine if mutator is None else phases.routine_mutation
    population = fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while attempts:
        for _ in range(selections):
            routine(population, selector, crosser, fitness, mutator, len(population), population[len(population)-1][1], distributor)
        best = population[0][1]
        population = death(population, remains)
        if population[0][1] >= best:
            attempts -= 1
        else:
            attempts = change
    return population


def expansor(population, selector, crosser, fitness, ratio, iterations=math.inf, max_element=math.inf, distributor=None, mutator=None):
    """ Expands the population until a max number of elements or for a limited number of iteraration.

    :param population: initial elements
    :param selector: function that select 2 element for crossover
    :param crosser: function to combine 2 BitArray
    :param fitness: function to estimate the usefulness of an element
    :param ratio: expansion ratio
    :param iterations: number of iteration, by default unlimited
    :param max_element: max number of elements, by default unlimited
    :return: final population sorted reversed by value
    :type ratio: float
    :type iterations: int
    :type fitness: Callable[[object], int]
    :type crosser: Callable[[BitArray, BitArray], BitArray]
    :type selector: Callable[[int],Tuple[int,int]]
    :type max_element: int
    :type population: list
    :rtype: List[Tuple[object, int]]
    """
    number = int(len(population) * ratio)
    routine = phases.routine if mutator is None else phases.routine_mutation
    population = fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while iterations and len(population) < max_element:
        for _ in range(number):
            routine(population, selector, crosser, fitness, mutator, len(population), population[len(population)-1][1], distributor)
        population = death(population, number)
        iterations -= 1
        number = int(number * ratio)
    return population


def restrictor(population, selector, crosser, fitness, ratio, iterations=math.inf, min_element=0, distributor=None, mutator=None):
    """ Reduce the population until a min number of elements or for a limited number of iteraration.

    :param population: initial elements
    :param selector: function that select 2 element for crossover
    :param crosser: function to combine 2 BitArray
    :param fitness: function to estimate the usefulness of an element
    :param ratio: reduction ratio
    :param iterations: number of iteration, by default unlimited
    :param min_element: min mumber of elements, by default 0
    :return: final population sorted reversed by value
    :type ratio: float
    :type iterations: int
    :type fitness: Callable[[object], int]
    :type crosser: Callable[[BitArray, BitArray], BitArray]
    :type selector: Callable[[int],Tuple[int,int]]
    :type min_element: int
    :type population: list
    :rtype: List[Tuple[object, int]]
    """
    number = int(len(population) * ratio)
    routine = phases.routine if mutator is None else phases.routine_mutation
    population = fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while iterations and len(population) > min_element:
        for _ in range(number):
            routine(population, selector, crosser, fitness, mutator, len(population), population[len(population)-1][1], distributor)
        population = death(population, number)
        iterations -= 1
        number = int(number * ratio)
    return population


def dynamic(population, change, selector, crosser, fitness, selection_ratio, death_ratio, min_element=0,
            max_element=math.inf, distributor=None, mutator=None):
    """Version of the algorithm where numbers of elements selected and discarted changes dinamically.

    :param population: initial elements
    :param change: number of iterations without the maximum value changes
    :param selector: function that select 2 element for crossover
    :param crosser: function to combine 2 BitArray
    :param fitness: function to estimate the usefulness of an element
    :param selection_ratio: expansion ratio
    :param death_ratio: reduction ratio
    :param min_element: min mumber of elements, by default 0
    :param max_element: max mumber of elements, by default unlimited
    :return: final population sorted reversed by value
    :type min_element: int
    :type max_element: int
    :type fitness: Callable[[object], int]
    :type crosser: Callable[[BitArray, BitArray], BitArray]
    :type selector: Callable[[int],Tuple[int,int]]
    :type min_element: int
    :type death_ratio: float
    :type selection_ratio: float
    :type population: list
    :rtype: List[Tuple[object, int]]
    """
    attempts = change
    routine = phases.routine if mutator is None else phases.routine_mutation
    selections = int(len(population) * selection_ratio)
    remains = int(len(population) * death_ratio)
    population = fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while attempts and min_element < len(population) < max_element:
        for _ in range(selections):
            routine(population, selector, crosser, fitness, mutator, len(population), population[len(population)-1][1], distributor)
        population = death(population, remains)
        best = population[0][1]
        if population[0][1] >= best:
            attempts -= 1
        else:
            attempts = change
        selections = int(selections * selection_ratio)
        remains = int(remains * death_ratio)
    return population
