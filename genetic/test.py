

from genetic import selectors, distributors, integer
from genetic.integer.algorithms import standard
from genetic.integer import mutators


def fitness(e):
    sum = 0
    for i in e:
        sum = sum + i
    return sum


def test():
    a = [1,2,3,4]
    b = [5,6,7,8]
    c = [9,10,11,12]
    d = [13,14,15,16]
    e = [17,18,19,20]

    p = [a,b,c,d,e]
    mutator = integer.mutators.add_mut
    p = standard(p, 7, selectors.random_sel, distributors.random_dist, distributors.random_dist, fitness, mutator, 5, 5, 25)
    print(p)


# def random_ev(population, change, fitness, mutator, selections=0, remains=0):
#     selections = len(population) if selections == 0 else selections
#     remains = len(population) if remains == 0 else remains
#     attempts = change
#     population = fit(population, fitness)
#     population.sort(key=lambda x: x[1], reverse=True)
#     while attempts:
#         generation = selection(population, selectors.random_sel, selections)
#         generation = cross(generation, distributors.random_dist)
#         generation = mutation(generation, distributors.random_dist, mutator)
#         generation = fit(generation, fitness)
#         generation = death(generation + population, remains)
#         if population[0][1] >= generation[0][1]:
#             attempts -= 1
#         else:
#             attempts = change
#         population = generation
#     return population



