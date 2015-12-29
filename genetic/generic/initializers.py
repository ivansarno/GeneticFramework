from typing import List, Tuple

__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """Function that create a new population"""

###
# type definition
Population = List[Tuple[object, int]]
###


def elements2population(elements, fitness) -> Population:
    """Take a list of elements and return a population in the format used by algorithms."""
    population = []
    for g in elements:
        population.append((g, fitness(g)))
    population.sort(key=lambda x: x[1], reverse=True)
    return population
