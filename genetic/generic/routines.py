import abc
from collections import Callable
from typing import Callable as Func, List, Tuple

from genetic.generic import distributors, selectors

__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """Standard routines for genetic algorithms"""


def random_routine(population, fitness, mutator, elements, minimum):
    distributor = distributors.random_dist
    selector = selectors.random_sel
    # selection
    indexes = selector(elements)
    first = population[indexes[0]][0]
    second = population[indexes[1]][0]
    # cross
    pivot = distributor(elements)
    new = first[0:pivot] + second[pivot:]
    # mutation
    mutator(new, distributor(len(new)))
    # fit
    value = fitness(new)
    if value > minimum:
        population.append((new, value))
    # cross
    pivot = distributor(elements)
    new = second[0:pivot] + first[pivot:]
    # mutation
    mutator(new, distributor(len(new)))
    # fit
    value = fitness(new)
    if value > minimum:
        population.append((new, value))


class Routine(Callable):
    @abc.abstractmethod
    def __call__(self, population: List[object], fitness: Func[[object], int], mutator):
        pass


class StandardRoutine(Routine):
    def __init__(self):
        self._distributor = distributors.random_dist
        self._selector = selectors.random_sel
        self._mut_distributor = distributors.random_dist

    def __call__(self, population: List[object], fitness: Func[[object], int], mutator):
        length = len(population) // 2
        for _ in range(length // 2 + 1):
            self._routine(population, fitness, mutator, length)

    def _routine(self, population, fitness, mutator, elements):
        # selection
        indexes = self._selector(elements)
        first = population[indexes[0]][0]
        second = population[indexes[1]][0]
        # cross
        pivot = self._distributor(len(first))
        new = first[0:pivot] + second[pivot:]
        # mutation
        mutator(new, self._mut_distributor(len(new)))
        # fit
        value = fitness(new)
        population.append((new, value))
        # cross
        new = second[0:pivot] + first[pivot:]
        # mutation
        mutator(new, self._mut_distributor(len(new)))
        # fit
        value = fitness(new)
        population.append((new, value))

    def set_distributor(self, distributor):
        self._distributor = distributor

    def set_mut_distributor(self, distributor):
        self._mut_distributor = distributor

    def set_selector(self, selector):
        self._selector = selector


class LazyRoutine(Routine):
    def __init__(self):
        self._distributor = distributors.random_dist
        self._selector = selectors.random_sel
        self._mut_distributor = distributors.random_dist

    def __call__(self, population: List[Tuple[object, int]], fitness: Func[[object], int], mutator):
        length = len(population) // 2
        minimum = population[-1][1]
        for _ in range(length // 2 + 1):
            self._routine(population, fitness, mutator, length, minimum)

    def _routine(self, population, fitness, mutator, elements, minimum):
        # selection
        indexes = self._selector(elements)
        first = population[indexes[0]][0]
        second = population[indexes[1]][0]
        # cross
        pivot = self._distributor(len(first))
        new = first[0:pivot] + second[pivot:]
        # mutation
        mutator(new, self._mut_distributor(len(new)))
        # fit
        value = fitness(new)
        if value > minimum:
            population.append((new, value))
        # cross
        new = second[0:pivot] + first[pivot:]
        # mutation
        mutator(new, self._mut_distributor(len(new)))
        # fit
        value = fitness(new)
        if value > minimum:
            population.append((new, value))

    def set_distributor(self, distributor):
        self._distributor = distributor

    def set_mut_distributor(self, distributor):
        self._mut_distributor = distributor

    def set_selector(self, selector):
        self._selector = selector


class MultiRoutine(Routine):
    def __init__(self):
        self._distributor = distributors.random_dist
        self._selector = selectors.reverse_multiselector
        self._mut_distributor = distributors.random_dist

    def __call__(self, population: List[Tuple[object, int]], fitness: Func[[object], int], mutator):
        generation = self._selector(population)
        self._routine(generation, population, fitness, mutator)

    def _routine(self, generation, population, fitness, mutator):
        for couple in generation:
            # selection
            first = couple[0]
            second = couple[0]
            # cross
            pivot = self._distributor(len(first))
            new = first[0:pivot] + second[pivot:]
            # mutation
            mutator(new, self._mut_distributor(len(new)))
            # fit
            value = fitness(new)
            population.append((new, value))
            # cross
            new = second[0:pivot] + first[pivot:]
            # mutation
            mutator(new, self._mut_distributor(len(new)))
            # fit
            value = fitness(new)
            population.append((new, value))

    def set_distributor(self, distributor):
        self._distributor = distributor

    def set_mut_distributor(self, distributor):
        self._mut_distributor = distributor

    def set_selector(self, multiselector):
        self._selector = multiselector


class LazyMultiRoutine(Routine):
    def __init__(self):
        self._distributor = distributors.random_dist
        self._selector = selectors.reverse_multiselector
        self._mut_distributor = distributors.random_dist

    def __call__(self, population: List[Tuple[object, int]], fitness: Func[[object], int], mutator):
        generation = self._selector(population)
        minimum = population[-1][1]
        self._routine(generation, population, fitness, mutator, minimum)

    def _routine(self, generation, population, fitness, mutator, minimum):
        for couple in generation:
            # selection
            first = couple[0]
            second = couple[0]
            # cross
            pivot = self._distributor(len(first))
            new = first[0:pivot] + second[pivot:]
            # mutation
            mutator(new, self._mut_distributor(len(new)))
            # fit
            value = fitness(new)
            if value > minimum:
                population.append((new, value))
            # cross
            new = second[0:pivot] + first[pivot:]
            # mutation
            mutator(new, self._mut_distributor(len(new)))
            # fit
            value = fitness(new)
            if value > minimum:
                population.append((new, value))

    def set_distributor(self, distributor):
        self._distributor = distributor

    def set_mut_distributor(self, distributor):
        self._mut_distributor = distributor

    def set_selector(self, multiselector):
        self._selector = multiselector
