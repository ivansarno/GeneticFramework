import functools
import math
from genetic import generic

__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """algorithm's versions specific for integer"""


def standard(population, change, selector, distributor1, distributor2, fitness, mutator, selections, remains,
             max_value):
    mutator = functools.partial(mutator, max_value=max_value)
    return generic.standard(population, change, selector, distributor1, distributor2, fitness, mutator, selections,
                            remains)


def standard_min(population, change, selector, distributor1, distributor2, fitness, mutator, selections, remains,
                 min_value, max_value):
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return generic.standard(population, change, selector, distributor1, distributor2, fitness, mutator, selections,
                            remains)


def expansor(population, selector, distributor1, distributor2, fitness, mutator, ratio, max_value, iterations=math.inf,
             max_element=math.inf):
    mutator = functools.partial(mutator, max_value=max_value)
    return generic.expansor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations,
                            max_element)


def expansor_min(population, selector, distributor1, distributor2, fitness, mutator, ratio, min_value, max_value,
                 iterations=math.inf,
                 max_element=math.inf):
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return generic.expansor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations,
                            max_element)


def restrictor(population, selector, distributor1, distributor2, fitness, mutator, ratio, max_value,
               iterations=math.inf,
               min_element=0):
    mutator = functools.partial(mutator, max_value=max_value)
    return generic.restrictor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations,
                              min_element)


def restrictor_min(population, selector, distributor1, distributor2, fitness, mutator, ratio, min_value, max_value,
                   iterations=math.inf,
                   min_element=0):
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return generic.restrictor(population, selector, distributor1, distributor2, fitness, mutator, ratio, iterations,
                              min_element)


def dynamic(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio, death_ratio,
            max_value,
            min_element=0, max_element=math.inf):
    mutator = functools.partial(mutator, max_value=max_value)
    return generic.dynamic(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio,
                           death_ratio, min_element, max_element)


def dynamic_min(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio,
                death_ratio, min_value, max_value,
                min_element=0, max_element=math.inf):
    mutator = functools.partial(mutator, max_value=max_value, min_value=min_value)
    return generic.dynamic(population, change, selector, distributor1, distributor2, fitness, mutator, selection_ratio,
                    death_ratio, min_element, max_element)
