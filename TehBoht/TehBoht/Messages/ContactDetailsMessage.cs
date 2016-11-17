using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TehBoht.Messages
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    using TehBoht.Dialogs;

    public static class MessageFactory
    {
        public static IMessageActivity CreateContactMessage(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Type = "message";
            message.Attachments = new List<Attachment>();
            List<CardImage> cardImages = new List<CardImage>();
            cardImages.Add(new CardImage(url: "http://www.goingpublic.de/wp-content/uploads/sites/2/2015/12/crossvertise-Geschaeftsfuehrer-1030x687.jpg"));
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction contactButton = new CardAction()
            {
                Value = "https://sitefinity.crossvertise.com/ueber-uns/kontakt",
                Type = "openUrl",
                Title = "Contact Form"
            };
            CardAction faqButton = new CardAction()
            {
                Value = "https://sitefinity.crossvertise.com/hilfe/faq",
                Type = "openUrl",
                Title = "FAQ"
            };
            cardButtons.Add(contactButton);
            cardButtons.Add(faqButton);
            HeroCard plCard = new HeroCard()
            {
                Title = "Crossvertise GmbH",
                Subtitle = "Contact Details",
                Text = $"012334-53455 \n | Mon-Friday \n | 8:00-17:00 \n | support@crossvertise.com",
                Images = cardImages,
                Buttons = cardButtons
            };
            var plAttachment = plCard.ToAttachment();
            message.Attachments.Add(plAttachment);
            return message;
        }

        public static IMessageActivity CreateSuggestionMessage(IDialogContext context, MediumTypes mediumType)
        {
            string image = "https://xvsitefinitycdn.azureedge.net/production/images/default-source/default-library/mediacatalog-ipad-desktop-plakat.png?sfvrsn=0";
            string link = "https://sitefinity.crossvertise.com/";
            switch (mediumType)
            {
                    case MediumTypes.Billboard:
                    image = "https://www.crossvertise.com/fileadmin/bilder/plakatformate/vorschau/city-light-poster.png";
                    link = "https://sitefinity.crossvertise.com/plakatwerbung";
                    break;
                case MediumTypes.Cinema:
                    image = "https://market.crossvertise.com/Content/Img/Media/add_service_cinema.png";
                    link = "https://sitefinity.crossvertise.com/kinowerbung";
                    break;
                    case MediumTypes.TV:
                    image = "https://www.crossvertise.com/fileadmin/bilder/TV.jpg";
                    link = "https://sitefinity.crossvertise.com/tv-werbung";
                    break;
            }

            var message = context.MakeMessage();
            message.Type = "message";
            message.Attachments = new List<Attachment>();
            List<CardImage> cardImages = new List<CardImage>();
            cardImages.Add(new CardImage(url: image));
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction linkButton = new CardAction()
            {
                Value = link,
                Type = "openUrl",
                Title = $"{mediumType} avertisement on crossvertise"
            };
           
            cardButtons.Add(linkButton);
            HeroCard plCard = new HeroCard()
            {
                Title = $"{mediumType} crossvertise",
                Subtitle = "Book now",
                Images = cardImages,
                Buttons = cardButtons
            };
            var plAttachment = plCard.ToAttachment();
            message.Attachments.Add(plAttachment);
            return message;
        }
    }
}