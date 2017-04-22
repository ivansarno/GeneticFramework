# GeneticFramework
Framework for Genetic Algorithm in F# 

**This project is not updated and consistent, the new version will soon be released with a new design**

The framework allows you to create complex functions that implements Genetic Algorithms,structured as a pipeline.
The framework provides several operators to build the pipeline. The design focuses on flexibility , efficiency is secondary, the purpose is to give the user the ability to experiment easily with various solutions to choose the appropriate algorithm.

**The framework consists of:**  

	-Evolution Algorithms
	-Mutation operators
	-Selection operators
	-Crossover operators
	-Generic Utils
	
**Types and Encodings**  
The framework is independent of the problem encodings. It provides built-in support for:  

	-Integers: Mutators, Initializers and Utils
	-Real: : Mutators, Initializers and Utils
	-BitString: : specific Crossers, Mutators, Initializers and Utils
	
**Examples**  
Examples available:   
	-Discrete Knapsack Problem (pipelining and configuration)  
	-Partitiotion (metaoperator)
