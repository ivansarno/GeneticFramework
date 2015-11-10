from typing import Callable, Tuple


def selection(population: list, selector:  Callable[[int], Tuple[int, int]], new: int) -> list:
    couples = []
    for _ in range(0, new):
        choice = selector(len(population))
        couples.append((population[choice[0]][0], population[choice[1]][0]))
    return couples


def cross(couples: list, distributor: Callable[[int], int]) -> list:
    generation = []
    for c in couples:
        choice = distributor(len(c))
        new = c[0][0:choice] + c[1][choice:]
        generation.append(new)
        new = c[1][0:choice] + c[0][choice:]
        generation.append(new)
    return generation


def fit(population, fitness: Callable[[int], int]):
    fitted = []
    for g in population:
        fitted.append((g, fitness(g)))
    return fitted


def death(population: list, remains: int) -> list:
    population.sort(key=lambda x: x[1], reverse=True)
    return population[0:remains]


def mutation(generation: list, distributor: Callable[[int], int], mutator: Callable) -> list:
    for g in generation:
        i = distributor(len(g))
        # noinspection PyCallingNonCallable
        g[i] = mutator(g[i])
    return generation
