using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PoseidonApi.Models;

namespace PoseidonApiTests;

public class RuleControllerTests
{
    private static ApiDbContext TestDbContext;

    [SetUp]
    public void Init()
    {
        TestDbContext = Setup.FakeRuleDbContext();
        TestDbContext.ChangeTracker.Clear();
    }

    [Test]
    public void Should_return_all_rules_when_read_action()
    {
        var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

        var getAllRules = ruleController.GetRule();

        Assert.That(getAllRules.Result.Value!.Count(), Is.EqualTo(2));
    }

    [TestCase(1)]
    [TestCase(20)]
    [Test]
    public void Should_return_expected_curve_point_when_get_rule_by_id(int id)
    {
        var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

        var getRuleById = ruleController.GetRule(id);

        Assert.That(getRuleById.Result.Value!.Id, Is.EqualTo(id));
    }

    [Test]
    public void Should_return_expected_newly_created_rule_when_post()
    {
        var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

        var ruleToAdd = ruleController.PostRule(
            new RuleDTO()
            {
                Description = "Description21",
                Json = "Json21",
                SqlStr = "SqlStr21",
                SqlPart = "SqlPart21"
            });

        Assert.That(ruleToAdd.Result.Result!.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
        Assert.That(TestDbContext.Rules.Count(), Is.EqualTo(3));
        Assert.That(TestDbContext.Rules.ToList()[2].Id, Is.EqualTo(21));
    }
    
    [Test, Order(1)]
    public void Should_return_expected_updated_rule_when_put()
    {
        var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

        var ruleToUpdate = ruleController.PutRule(id: 20,
            new RuleDTO()
            {
                Id = 20,
                Description = "DescriptionUpdated",
                Json = "JsonUpdated",
                SqlStr = "SqlStrUpdated",
                SqlPart = "SqlPartUpdated"
            }, TestDbContext);

        Assert.That(ruleToUpdate.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
        Assert.That(TestDbContext.Rules.Count(), Is.EqualTo(2)); //Ensure no POST
        Assert.That(TestDbContext.Rules.ToList().Find(c => c.Id == 20).Description,
            Is.EqualTo("DescriptionUpdated"));
    }

    [Test]
    public void Should_return_expected_deleted_rule_when_delete()
    {
        var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

        var ruleToDelete = ruleController.DeleteRule(1);

        Assert.That(ruleToDelete.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
        Assert.That(TestDbContext.Rules.Count(), Is.EqualTo(1));
        Assert.That(TestDbContext.Rules.ToList().Find(c => c.Id == 1), Is.Null);
    }
}
