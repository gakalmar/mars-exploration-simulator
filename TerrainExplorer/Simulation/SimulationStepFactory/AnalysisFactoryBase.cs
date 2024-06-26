﻿using TerrainExplorer.Simulation.SimulationSteps;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.Simulation.SimulationStepFactory;

public class AnalysisFactoryBase : SimulationStepFactoryBase
{
    public override SimulationStepBase Create(SimulationContext simulationContext, ICoordinateCalculator coordinateCalculator)
    {
        return new Analysis(simulationContext, coordinateCalculator);
    }
}