from genetic import selectors

from genetic import distributors
from genetic.steps import selection, cross, mutation, fit, death


def complete(population, change, selector, distributor1, distributor2, fitness, mutator, selections, remains):
    attempts = change
    population = fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while attempts:
        generation = selection(population, selector, selections)
        generation = cross(generation, distributor1)
        generation = mutation(generation, distributor2, mutator)
        generation = fit(generation, fitness)
        generation = death(generation + population, remains)
        if population[0][1] >= generation[0][1]:
            attempts -= 1
        else:
            attempts = change
        population = generation
    return population


def random_ev(population, change, fitness, mutator, selections=0, remains=0):
    selections = len(population) if selections == 0 else selections
    remains = len(population) if remains == 0 else remains
    attempts = change
    population = fit(population, fitness)
    population.sort(key=lambda x: x[1], reverse=True)
    while attempts:
        generation = selection(population, selectors.random_sel, selections)
        generation = cross(generation, distributors.random_dist)
        generation = mutation(generation, distributors.random_dist, mutator)
        generation = fit(generation, fitness)
        generation = death(generation + population, remains)
        if population[0][1] >= generation[0][1]:
            attempts -= 1
        else:
            attempts = change
        population = generation
    return population
