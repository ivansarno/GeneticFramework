from genetic.phases import selection, cross, mutation, fit, death
__author__ = 'ivansarno'
__version__ = 'preview'
__doc__ = """Abstract Genetic Algorithm"""


def custom(population, change, selector, distributor1, distributor2, fitness, mutator, selections, remains):
    """ Abstract Genetic Algorithm, fully customizable

    :param population: initial elements
    :param change: number of iterations without the maximum value changes
    :param selector: function that select 2 element for crossover
    :param distributor1: function to select a pivot to crossover
    :param distributor2: function to select a gene to mutation
    :param fitness: function to estimate the usefulness of an element
    :param mutator: function that applies a muntation on a gene
    :param selections: number of couples created by selection phase (number of new element is douple)
    :param remains: number of elelement preserved for new iteration
    :return:
    """
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
