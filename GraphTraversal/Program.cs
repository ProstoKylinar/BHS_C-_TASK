using System;
using System.Collections.Generic;

public class Connection
{
    public Slot Source { get; set; }
    public Slot Target { get; set; }

    public Connection(Slot source, Slot target)
    {
        Source = source;
        Target = target;
        source.Connections.Add(this);
        target.Connections.Add(this);
    }
}

public abstract class Slot
{
    public List<Connection> Connections { get; } = new List<Connection>();
    public Block Block { get; }

    protected Slot(Block block)
    {
        Block = block;
    }
}

public class InputSlot : Slot
{
    public InputSlot(Block block) : base(block) { }
}

public class OutputSlot : Slot
{
    public double? Data { get; set; }

    public OutputSlot(Block block) : base(block) { }
}

public abstract class Block
{
    public InputSlot GraphInput { get; }
    public OutputSlot GraphOutput { get; }
    public List<InputSlot> DataInputs { get; } = new List<InputSlot>();
    public List<OutputSlot> DataOutputs { get; } = new List<OutputSlot>();

    protected Block()
    {
        GraphInput = new InputSlot(this);
        GraphOutput = new OutputSlot(this);
    }

    public abstract void Run();
}

public class ConstBlock : Block
{
    private readonly double _value;

    public ConstBlock(double value)
    {
        _value = value;
        DataOutputs.Add(new OutputSlot(this) { Data = value });
    }

    public override void Run()
    {
        Console.WriteLine($"ConstBlock: Outputting {_value}");
    }
}

// добавлены отладочные выводы, с помощью которых отслеживаем устновку связей
public class AdderBlock : Block
{
    public AdderBlock()
    {
        DataInputs.Add(new InputSlot(this));
        DataInputs.Add(new InputSlot(this));
        DataOutputs.Add(new OutputSlot(this));
    }

    public override void Run()
    {
        Console.WriteLine("AdderBlock: Running...");
        if (DataInputs[0].Connections.Count > 0 && DataInputs[1].Connections.Count > 0)
        {
            var input1 = DataInputs[0].Connections[0].Source as OutputSlot;
            var input2 = DataInputs[1].Connections[0].Source as OutputSlot;

            if (input1?.Data.HasValue == true && input2?.Data.HasValue == true)
            {
                double result = input1.Data.Value + input2.Data.Value;
                DataOutputs[0].Data = result;
                Console.WriteLine("AdderBlock: Calculated Sum = " + result);
            }
            else
            {
                Console.WriteLine("AdderBlock: Missing input data.");
            }
        }
        else
        {
            Console.WriteLine("AdderBlock: Missing connections.");
        }

        foreach (var connection in GraphOutput.Connections)
        {
            connection.Target.Block.Run();
        }
    }
}

public class PrinterBlock : Block
{
    public PrinterBlock()
    {
        DataInputs.Add(new InputSlot(this));
    }

    public override void Run()
    {
        Console.WriteLine("PrinterBlock: Running...");
        if (DataInputs[0].Connections.Count > 0)
        {
            var input = DataInputs[0].Connections[0].Source as OutputSlot;

            if (input?.Data.HasValue == true)
            {
                Console.WriteLine("PrinterBlock: Output = " + input.Data.Value);
            }
            else
            {
                Console.WriteLine("PrinterBlock: No data available.");
            }
        }
        else
        {
            Console.WriteLine("PrinterBlock: No connections.");
        }
    }
}

public class EntryPointBlock : Block
{
    public override void Run()
    {
        Console.WriteLine("EntryPointBlock: Starting...");
        foreach (var connection in GraphOutput.Connections)
        {
            Console.WriteLine("EntryPointBlock: Passing control to next block...");
            connection.Target.Block.Run();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var entryPoint = new EntryPointBlock();
        var const1 = new ConstBlock(5);
        var const2 = new ConstBlock(10);
        var adder = new AdderBlock();
        var printer = new PrinterBlock();

        new Connection(entryPoint.GraphOutput, adder.GraphInput);
        new Connection(adder.GraphOutput, printer.GraphInput);

        new Connection(const1.DataOutputs[0], adder.DataInputs[0]);
        new Connection(const2.DataOutputs[0], adder.DataInputs[1]);
        new Connection(adder.DataOutputs[0], printer.DataInputs[0]);

        Console.WriteLine("Starting graph traversal...");
        entryPoint.Run();
    }
}
