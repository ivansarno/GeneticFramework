import functools
import math
from genetic.generic import algorithms as gen

__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """algorithm's versions specific for integer"""


def standard(population, change, selector, distributor1, distributor2, fitness, mutator, selections, remains,
             max_value, min_value=0, ):
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return gen.standard(population, change, selector, distributor1, distributor2, fitness, mutator, selections,
                        remains)


def expansor(population, selector, distributor1, distributor2, fitness, mutator, ratio, max_value, min_value=0,
             iterations=math.inf, max_element=math.inf):
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return gen.expansor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations,
                        max_element)


def restrictor(population, selector, distributor1, distributor2, fitness, mutator, ratio, max_value, min_value=0,
               iterations=math.inf, min_element=0):
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return gen.restrictor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations,
                          min_element)


def dynamic(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio,
            death_ratio, max_value, min_value=0, min_element=0, max_element=math.inf):
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return gen.dynamic(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio,
                       death_ratio, min_element, max_element)
