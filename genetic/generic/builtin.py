from genetic.generic import algorithms, routines

__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """Built-in Genetic Algorithm"""


def random_standard(population, fitness, mutator, change=10):
    routine = routines.random_routine
    return algorithms.standard(population, fitness, mutator, routine, change)
