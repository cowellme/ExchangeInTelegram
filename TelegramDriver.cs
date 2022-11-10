using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleAppTG
{
    public class TelegramDriver
    {
        #region Variables
         
        public static ITelegramBotClient bot = new TelegramBotClient("5691063684:AAFHxZsOeeE7exdRiwJWfarT4oD4XByDMRs");
        public static int client = 0;
        public static bool price = false, count = false, country = false;
        public static double cost, countd;
        #endregion

        #region StartFunc
        public static void Start()
        {
            try
            {
                var cts = new CancellationTokenSource();
                var cancellationToken = cts.Token;
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = { },
                };
                bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
                Logger.Info("All good  ");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region Markups
            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "" } }) { ResizeKeyboard = true };
            ReplyKeyboardMarkup replyKeyboardMarkupMain = new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "Создать заявку", "Просмотреть", "Список заявок" }, new KeyboardButton[] { "Создать заявку", "Админ", "Рефералка" } }) { ResizeKeyboard = true };
            ReplyKeyboardMarkup replyKeyboardMarkupExc = new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "Покупатель", "Продавец" }, new KeyboardButton[] { "Назад" } }) { ResizeKeyboard = true };
            #endregion

            #region Inlines
            InlineKeyboardMarkup InlineKeyboardSeller1 = (new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "BTC", callbackData: $"btc") }, });


            InlineKeyboardMarkup InlineKeyboardBuyer1 = (new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "USDT TRC", callbackData: $"usdt") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "USDT SOL", callbackData: $"usdt") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "USDT ERC", callbackData: $"usdt") },});

            InlineKeyboardMarkup InlineKeyboardBuyer2 = (new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "CNY", callbackData: $"cny"), InlineKeyboardButton.WithCallbackData(text: "USD", callbackData: $"usd") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "RUB", callbackData: $"rub"), InlineKeyboardButton.WithCallbackData(text: "EUR", callbackData: $"eur") },});


            InlineKeyboardMarkup InlineKeyboardSeller0 = (new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "BTC", callbackData: $"btc") }, });
            InlineKeyboardMarkup InlineKeyboardBuyer0 = (new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "USDT", callbackData: $"usdtb") }, });


            InlineKeyboardMarkup InlineKeyboardMain = (new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "", callbackData: $"1") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "Создать заявку", callbackData: $"create") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "Просмотреть", callbackData: $"watch") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "Список заявок", callbackData: $"list") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "Админ", callbackData: $"admin") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "Рефералка", callbackData: $"refferal") } });


            InlineKeyboardMarkup InlineKeyboardCreate = (new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Покупатель", callbackData: $"buyer") },
                new[] { InlineKeyboardButton.WithCallbackData(text: "Продавец", callbackData: $"seller") },});


            InlineKeyboardMarkup InlineKeyboardDefault = (new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "⭐️", callbackData: $"1") }, });
            #endregion
            
            try
            {

                #region Callback
                if (update.CallbackQuery != null)
                {

                    string str = update.CallbackQuery.Data;
                    long TID = update.CallbackQuery.From.Id;

                    if (str == "create")
                    {
                        await bot.SendTextMessageAsync(TID, "Кто вы в сделке ?", replyMarkup: InlineKeyboardCreate);
                    }

                    #region SellerLine

                    if (str == "seller")
                    {
                        await bot.SendTextMessageAsync(TID, "Что вы продаете ?", replyMarkup: InlineKeyboardSeller0);
                    }

                    if (str == "btc")
                    {
                        await bot.SendTextMessageAsync(TID, "Укажите валюту вашей платежки:", replyMarkup: InlineKeyboardBuyer2);
                    }

                    if (str == "cny")
                    {
                        await bot.SendTextMessageAsync(TID, "Укажите по какому курсу будет обмен:");
                        price = true;
                    }
                    if (str == "rub")
                    {
                        await bot.SendTextMessageAsync(TID, "Укажите по какому курсу будет обмен:");
                        price = true;
                    }
                    if (str == "usd")
                    {
                        await bot.SendTextMessageAsync(TID, "Укажите по какому курсу будет обмен:");
                        price = true;
                    }
                    if (str == "eur")
                    {
                        await bot.SendTextMessageAsync(TID, "Укажите по какому курсу будет обмен:");
                        price = true;
                    }

                    #endregion
                    if (str == "usdtb")
                    {
                        await bot.SendTextMessageAsync(TID, "Чем оплачиваете ?\n" +
                                "#Список#", replyMarkup: InlineKeyboardBuyer2);
                    }

                    

                    

                    

                    if (str == "buyer")
                    {
                        await bot.SendTextMessageAsync(TID, "Что вы покупаете ?", replyMarkup: InlineKeyboardBuyer0);
                    }

                    
                }
                #endregion

                #region Buttons

                if (update.Type == UpdateType.Message)
                {
                    var message = update.Message;
                    long TID = message.Chat.Id;
                    if (message.Text != null)
                    {
                        #region Start
                        if (message.Text.ToLower() == "/start")
                        {
                            await bot.SendTextMessageAsync(TID, "Exchange:\n" +
                                "Neteller...", replyMarkup: InlineKeyboardMain);
                        }
                        #endregion


                        if (message.Text == "Назад")
                        {
                            await bot.SendTextMessageAsync(TID, "Exchange:\n" +
                                "#Список", replyMarkup: InlineKeyboardMain);
                        }


                        if (country && message != null)
                        {
                            try
                            {
                                country = false;
                                string cou = message.Text;
                                if (cou.Length >= 21)
                                {
                                    await bot.SendTextMessageAsync(TID, "Название не больше 20 символов!");
                                    return;
                                }
                                await bot.SendTextMessageAsync(TID, $"Вы указали страну {cou}");
                                country = true;

                            }
                            catch
                            {
                                await bot.SendTextMessageAsync(TID, "Некорректный ввод кол-ва!");
                            }
                        }

                        if (count && message != null)
                        {
                            try
                            {
                                count = false;
                                countd = Convert.ToDouble(message.Text);
                                await bot.SendTextMessageAsync(TID, $"Вы указали кол-во {countd}" +
                                    $"\n" +
                                    $"\n" +
                                    $"Укажите страну платежной системы:");
                                country = true;

                            }
                            catch
                            {
                                await bot.SendTextMessageAsync(TID, "Некорректный ввод кол-ва!");
                            }
                        }

                        if (price && message != null)
                        {
                            try
                            {
                                price = false;
                                cost = Convert.ToDouble(message.Text);
                                await bot.SendTextMessageAsync(TID, $"Вы указали {cost} курс" +
                                    $"\n" +
                                    $"\n" +
                                    $"Укажите кол-во продаваемой валюты:");
                                count = true;
                            }
                            catch
                            {
                                await bot.SendTextMessageAsync(TID, "Некорректный ввод курса!");
                            }
                        }

                        
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Logger.Error(exception); });
        }
         
         
    }       
}
