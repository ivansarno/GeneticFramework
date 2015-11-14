import math

from genetic import generic
from genetic.bit import phases

__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """algorithm's versions specific for bits sequences"""


def standard(population, change, selector, distributor1, distributor2, fitness, mutator, selections, remains):
    """ Abstract Genetic Algorithm, fully customizable.

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
    """
    attempts = change
    population = generic.fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while attempts:
        generation = generic.selection(population, selector, selections)
        generation = phases.cross(generation, distributor1)
        generation = phases.mutation(generation, distributor2, mutator)
        generation = generic.fit(generation, fitness)
        generation = generic.death(generation + population, remains)
        if population[0][1] >= generation[0][1]:
            attempts -= 1
        else:
            attempts = change
        population = generation
    return population


def expansor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations=math.inf,
             max_element=math.inf):
    """ Expands the population until a max number of elements or for a limited number of iteraration.

    :param population: initial elements
    :param selector: function that select 2 element for crossover
    :param distributor1: function to select a pivot to crossover
    :param distributor2: function to select a gene to mutation
    :param fitness: function to estimate the usefulness of an element
    :param mutator: function that applies a muntation on a gene
    :param ratio: expansion ratio
    :param iterations: number of iteration
    :param max_element: max number of elements
    :return: final population sorted reversed by value
    """
    number = int(len(population) * ratio)
    population = generic.fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while iterations and len(population) < max_element:
        generation = generic.selection(population, selector, number)
        generation = phases.cross(generation, distributor1)
        generation = phases.mutation(generation, distributor2, mutator)
        generation = generic.fit(generation, fitness)
        generation = generic.death(generation + population, number)
        iterations -= 1
        number = int(number * ratio)
        population = generation
    return population


def restrictor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations=math.inf,
               min_element=0):
    """ Reduce the population until a min number of elements or for a limited number of iteraration.

    :param population: initial elements
    :param selector: function that select 2 element for crossover
    :param distributor1: function to select a pivot to crossover
    :param distributor2: function to select a gene to mutation
    :param fitness: function to estimate the usefulness of an element
    :param mutator: function that applies a muntation on a gene
    :param ratio: reduction ratio
    :param iterations: number of iteration
    :param min_element: min mumber of elements
    :return: final population sorted reversed by value
    """
    number = int(len(population) * ratio)
    population = generic.fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while iterations and len(population) > min_element:
        generation = generic.selection(population, selector, number)
        generation = phases.cross(generation, distributor1)
        generation = phases.mutation(generation, distributor2, mutator)
        generation = generic.fit(generation, fitness)
        generation = generic.death(generation + population, number)
        iterations -= 1
        number = int(number * ratio)
        population = generation
    return population


def dynamic(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio, death_ratio,
            min_element=0, max_element=math.inf):
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
    :param min_element: min mumber of elements
    :param max_element: max mumber of elements
    :return: final population sorted reversed by value
    """
    attempts = change
    selections = int(len(population) * selection_ratio)
    remains = int(len(population) * death_ratio)
    population = generic.fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while attempts and min_element < len(population) < max_element:
        generation = generic.selection(population, selector, selections)
        generation = phases.cross(generation, distributor1)
        generation = phases.mutation(generation, distributor2, mutator)
        generation = generic.fit(generation, fitness)
        generation = generic.death(generation + population, remains)
        if population[0][1] >= generation[0][1]:
            attempts -= 1
        else:
            attempts = change
        selections = int(selections * selection_ratio)
        remains = int(remains * death_ratio)
        population = generation
    return population
