using System;

namespace TehBoht.Dialogs
{
    using Microsoft.Bot.Builder.FormFlow;

    public enum Geography
    {
       NotSpecified, Local, Regional, CrossRegional, National
    }

    public enum Theme
    {
        General, SpecialInterest, Professionals
    }

    public enum Impulses
    {
        Short, ShortMedium, Medium, MediumLong, Long
    }

    public enum Complexity
    {
        Simple, RatherSimple, Average, RatherComplex, Complex
    }

    public enum ProductClass
    {
        Cheap, RatherCheap, Middle, RatherHigh, High
    }

    public enum MediumTypes
    {
       Billboard, Cinema, TV
    }

    [Serializable]
    [Template(TemplateUsage.NotUnderstood, " \"{0}\" is a pretty big word for a crossvertise developer. Imagine how that confuses me! Try again.", "Woa there XV Devs, come again...  \"{0}\" makes no sense.")]
    [Template(TemplateUsage.EnumSelectOne, "How would you describe the {&} in regards to your campaign? {||}")]
    public class MediaCompassDialog
    {
        [Prompt("What would the reach of your campaign {&} ideally be? {||}")]
        public Geography? Geography;

        [Prompt("How would you describe your campaign {&}? {||}")]
        public Theme? Theme;

        [Prompt("What would the buyer {&} be? {||}")]
        public Impulses? Impulses;

        public MediumTypes? SuggestedMediumType;

        [Optional]
        public string SuggestedLink;

        public static IForm<MediaCompassDialog> BuildForm()
        {
            return new FormBuilder<MediaCompassDialog>()
                .Confirm("I can help narrow down your choices with a couple of question. Is that okay?")
                .Field(nameof(Theme))
                .Field(nameof(Geography))
                .Field(nameof(Impulses))
                .Confirm(
                    async (state) =>
                        {
                            switch (state.Geography)
                            {
                                case Dialogs.Geography.National:
                                    state.SuggestedMediumType = MediumTypes.TV;
                                    state.SuggestedLink = "LOLOLOl";
                                    break;
                                case Dialogs.Geography.CrossRegional:
                                    state.SuggestedMediumType = MediumTypes.Billboard;
                                    break;
                                case Dialogs.Geography.Regional:
                                    state.SuggestedMediumType = MediumTypes.Cinema;
                                    break;
                            }
                            return new PromptAttribute($"I can recommend {state.SuggestedMediumType} advertising. That works right?");
                        })
                        .Message("Let me know if you need anything")
                        .Build();
        }



    }
}