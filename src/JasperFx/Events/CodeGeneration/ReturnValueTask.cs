using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Frames;
using JasperFx.CodeGeneration.Model;
using JasperFx.Core.Reflection;

namespace JasperFx.Events.CodeGeneration;

public class ReturnValueTask: SyncFrame
{
    private readonly Type _variableType;
    private Variable _returnValue;

    public ReturnValueTask(Type variableType)
    {
        _variableType = variableType;
    }

    public override IEnumerable<Variable> FindVariables(IMethodVariables chain)
    {
        _returnValue = chain.FindVariable(_variableType);
        yield return _returnValue;
    }

    public override void GenerateCode(GeneratedMethod method, ISourceWriter writer)
    {
        writer.WriteLine(
            $"return new {typeof(ValueTask).FullNameInCode()}<{_variableType.FullNameInCode()}>({_returnValue.Usage});");
    }
}
