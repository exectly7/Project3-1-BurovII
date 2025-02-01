﻿/*
 *
 *
 * 
 */
using Project3_1.Lib;
using Project3_1.Lib.JsonModels;

namespace Project3_1.Core
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Main()
        {
            string json = "{\n    \"elements\": [\n        {\n            \"id\": \"_ability.fatigued\",\n\t\t\t\"aspects\": {\"ability\":1,\"fatigued\":1},\n            \"noartneeded\":true\n        },\n\n        {\n            \"ID\": \"xcho\",\n            \"label\": \"Хор\",\n            \"desc\": \"Рвение. Инстинкт. Ритм. [Одна из девяти частей души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"heart\":2,\"grail\":1,\"boost.heart\":2,\"boost.grail\":1,\".cho\":1},\n            \"xtriggers\":{\"fatiguing\":\"zcho\",\"fatiguing.ability\":\"zcho\",\"malady.inflicting\":\"mcho\",\"contamination.bloodlines\":\"mcho\",\"contamination.keeperskin\":\"mcho\"}\n            \n        },\n        {\n            \"id\": \"xere\",\n            \"label\": \"Эреб\",\n            \"desc\": \"Гордыня и сострадание. Ненависть и страх. [Одна из девяти частей души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"grail\":2,\"edge\":1,\"boost.grail\":2,\"boost.edge\":1,\".ere\":1},\n            \"xtriggers\":{\"fatiguing\":\"zere\",\"fatiguing.ability\":\"zere\",\"malady.inflicting\":\"mere\",\"contamination.bloodlines\":\"mere\",\"contamination.keeperskin\":\"mere\"}\n        \n        },\n        {\n            \"id\": \"xfet\",\n            \"label\": \"Фет\",\n            \"desc\": \"Та наша часть, что странствует во снах. [Одна из девяти частей души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"rose\":2,\"moth\":1,\"boost.rose\":2,\"boost.moth\":1,\".fet\":1},\n            \"xtriggers\":{\"fatiguing\":\"zfet\",\"fatiguing.ability\":\"zfet\",\"malady.inflicting\":\"mfet\",\"contamination.curse.fifth.eye\":\"mfet\",\"contamination.winkwell\":\"mfet\"}\n        },\n        {\n            \"id\": \"xhea\",\n            \"label\": \"Здоровье\",\n            \"desc\": \"Вместилище души. [Девятая, осязаемая часть души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"heart\":1,\"scale\":1,\"nectar\":1,\"boost.heart\":1,\"boost.scale\":1,\"boost.nectar\":1,\".hea\":1},\n            \"xtriggers\":{\"fatiguing\":\"zhea\",\"fatiguing.ability\":\"zhea\",\"malady.inflicting\":\"mhea\",\"contamination.chionic\":\"mhea\",\"contamination.sthenic.taint\":\"mhea\"}\n        },\n        {\n            \"id\": \"xhea.halfdrowned\",\n            \"label\": \"Здоровье [озноб]\",\n            \"desc\": \"Я промок до костей и замёрз. [Вас бьёт крупная дрожь. Ищите место, где вас обогреют.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"soaked\":1,\"heart\":1,\"winter\":1,\"ability\":1,\"malady\":1},\n            \"xtriggers\":{\"drying\":\"xhea\"},\n            \"lifetime\":360,\n            \"icon\":\"xhea\",\n            \"decayto\": \"xhea.freezing\"\n        },\n        {\n            \"id\": \"xhea.freezing\",\n            \"label\": \"Здоровье [переохлаждение]\",\n            \"desc\": \"Я уже не чувствую холода. Я мог бы лечь прямо тут. Лечь. Уснуть. [Вам нужно в тепло. Как можно скорее.]\",\n            \"aspects\": {\"rank\":12,\"soaked\":1,\"winter\":1,\"ability\":1,\"malady\":1},\n            \"xtriggers\":{\"drying\":\"xhea\"},\n            \"icon\":\"xhea\",\n            \"lifetime\":360,\n            \"decayto\": \"xhea.freezing\"\n        },\n        {\n            \"id\": \"xmet\",\n            \"label\": \"Решимость\",\n            \"desc\": \"Воля. Самодисциплина. Та наша часть, что выбирает путь. [Одна из девяти частей души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"forge\":2,\"edge\":1,\"boost.forge\":2,\"boost.edge\":1,\".met\":1},\n            \"xtriggers\":{\"fatiguing\":\"zmet\",\"fatiguing.ability\":\"zmet\"}\n        },\n        {\n            \"id\": \"xpho\",\n            \"label\": \"Фост\",\n            \"desc\": \"Зрение. Чувства. Наитие. Всё, чем наделила нас Слава. [Одна из девяти частей души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"lantern\":2,\"sky\":1,\"boost.lantern\":2,\"boost.sky\":1,\".pho\":1},\n            \"xtriggers\":{\"fatiguing\":\"zpho\",\"fatiguing.ability\":\"zpho\",\"malady.inflicting\":\"mpho\",\"contamination.curse.fifth.eye\":\"mpho\",\"contamination.actinic\":\"mpho\"}\n        },\n        {\n            \"id\": \"xsha\",\n            \"label\": \"Шапт\",\n            \"desc\": \"Слова и разумение. И вход, и выход суть одна и та же Дверь. [Одна из девяти частей души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"knock\":2,\"forge\":1,\"boost.knock\":2,\"boost.forge\":1,\".sha\":1},\n            \"xtriggers\":{\"fatiguing\":\"zsha\",\"fatiguing.ability\":\"zsha\",\"malady.inflicting\":\"msha\",\"contamination.sthenic.taint\":\"msha\",\"contamination.witchworms\":\"msha\"}\n\n        },\n        {\n            \"id\": \"xtri\",\n            \"label\": \"Трист\",\n            \"desc\": \"Непостоянство и томление. [Одна из девяти частей души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"moth\":2,\"moon\":1,\"boost.moth\":2,\"boost.moon\":1,\".tri\":1},\n            \"xtriggers\":{\"fatiguing\":\"ztri\",\"fatiguing.ability\":\"ztri\",\"malady.inflicting\":\"mtri\",\"contamination.chionic\":\"mtri\",\"contamination.actinic\":\"mtri\"}\n        },\n        {\n            \"id\": \"xwis\",\n            \"label\": \"Вист\",\n            \"desc\": \"Имя. Память. То, что остаётся. Мы знаем, что у книг есть души. О, если бы нам попадались книги с одним лишь вистом! [Одна из девяти частей души человека.]\",\n\t\t\t\"aspects\": {\"rank\":12,\"campable\":1,\"ability\":1,\"winter\":2,\"lantern\":1,\"boost.winter\":2,\"boost.lantern\":1,\".wis\":1},\n            \"xtriggers\":{\"fatiguing\":\"zwis\",\"fatiguing.ability\":\"zwis\",\"malady.inflicting\":\"mwis\",\"contamination.witchworms\":\"mwis\",\"contamination.winkwell\":\"mwis\"}\n        },\n        {\n            \"id\": \"zcho\",\n            \"label\": \"Хор [изнурён]\",\n            \"desc\": \"Рвение. Инстинкт. Ритм.\",\n            \"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zheart\":2,\"zgrail\":1},\n            \"xtriggers\":{\"recovering\":\"xcho\",\"recovering.ability\":\"xcho\",\"malady.inflicting\":\"mcho\",\"contamination.bloodlines\":\"mcho\",\"contamination.keeperskin\":\"mcho\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"zere\",\n            \"label\": \"Эреб [изнурён]\",\n            \"desc\": \"Гордыня и сострадание. Ненависть и страх.\",\n            \"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zgrail\":2,\"zedge\":1},\n            \"xtriggers\":{\"recovering\":\"xere\",\"recovering.ability\":\"xere\",\"malady.inflicting\":\"mere\",\"contamination.bloodlines\":\"mere\",\"contamination.keeperskin\":\"mere\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"zfet\",\n            \"label\": \"Фет [изнурён]\",\n            \"desc\": \"Та наша часть, что странствует во снах.\",\n            \"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zrose\":2,\"zmoth\":1},\n            \"xtriggers\":{\"recovering\":\"xfet\",\"recovering.ability\":\"xfet\",\"malady.inflicting\":\"mfet\",\"contamination.winkwell\":\"mfet\",\"contamination.curse.fifth.eye\":\"mfet\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"zhea\",\n            \"label\": \"Здоровье [изнурено]\",\n            \"desc\": \"Вместилище души.\",\n            \"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zheart\":1,\"znectar\":1,\"zscale\":1},\n            \"xtriggers\":{\"recovering\":\"xhea\",\"recovering.ability\":\"xhea\",\"malady.inflicting\":\"mhea\",\"contamination.chionic\":\"mhea\",\"contamination.sthenic.taint\":\"mhea\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"zmet\",\n            \"label\": \"Решимость [изнурена]\",\n            \"desc\": \"Способность действовать в решающий момент.\",\n    \t\t\"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zforge\":2,\"zedge\":1},\n            \"xtriggers\":{\"recovering\":\"xmet\",\"recovering.ability\":\"xmet\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"zpho\",\n            \"label\": \"Фост [изнурён]\",\n            \"desc\": \"Зрение. Чувства. Наитие. Всё, чем наделила нас Слава.\",\n            \"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zlantern\":2,\"zsky\":1},\n            \"xtriggers\":{\"recovering\":\"xpho\",\"recovering.ability\":\"xpho\",\"malady.inflicting\":\"mpho\",\"contamination.curse.fifth.eye\":\"mpho\",\"contamination.actinic\":\"mpho\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"zsha\",\n            \"label\": \"Шапт [изнурён]\",\n            \"desc\": \"Слова и разумение. И вход, и выход суть одна и та же Дверь.\",\n            \"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zknock\":2,\"zforge\":1},\n            \"xtriggers\":{\"recovering\":\"xsha\",\"recovering.ability\":\"xsha\",\"malady.inflicting\":\"msha\",\"contamination.sthenic.taint\":\"msha\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"ztri\",\n            \"label\": \"Трист [изнурён]\",\n            \"desc\": \"Непостоянство и томление.\",\n            \"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zmoth\":2,\"zmoon\":1},\n            \"xtriggers\":{\"recovering\":\"xtri\",\"recovering.ability\":\"xtri\",\"malady.inflicting\":\"mtri\",\"contamination.chionic\":\"mtri\",\"contamination.actinic\":\"mtri\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"zwis\", \n            \"label\": \"Вист [изнурён]\",\n            \"desc\": \"Имя. Память. То, что остаётся. (Мы знаем, что у книг есть души. О, если бы нам попадались книги с одним лишь вистом!)\",\n            \"inherits\": \"_ability.fatigued\",\n            \"aspects\": {\"rank\":1,\"zwinter\":2,\"zlantern\":1},\n            \"xtriggers\":{\"recovering\":\"xwis\",\"recovering.ability\":\"xwis\",\"malady.inflicting\":\"mwis\",\"contamination.witchworms\":\"mwis\",\"contamination.winkwell\":\"mwis\"},\n            \"resaturate\": true\n        },\n        {\n            \"id\": \"mcho\",\n            \"label\": \"Хор: дуэндратизм\",\n            \"desc\": \"Дуэндратизм. Болезнь помешанных, поэтов и одержимых. В вое ветра, шуме волн и треске поленьев есть музыка, что слышна лишь мне. Когда я ей внимаю, мир рдеет, словно роза.\",\n            \"xtriggers\":{\"malady.curing\":\"zcho\",\"contamination.bloodlines\":[{\"id\":\"ability.exposed.bloodlines\",\"morpheffect\":\"mutate\",\"level\":1}],\"contamination.keeperskin\":[{\"id\":\"ability.exposed.keeperskin\",\"morpheffect\":\"mutate\",\"level\":1}]},\n            \"xexts\":{\"malady.inflicting\":\"Я не в силах обуздать мысли и слышу, будто вдали бьют в барабан. [Ваш хор постиг недуг.]\",\n            \"contamination.bloodlines\":\"Я чувствую зуд в кончиках пальцев. Вот они уже выстукивают ритм. Эта книга заражена гемоглоссами, и там, где паразиты разъели оболочку моей души, к ней пристал дуэндразон. [Ваш хор постиг недуг; предмет оказался заражён паразитом.]\",\n            \"contamination.keeperskin\":\"Кончики моих пальцев распухли и побелели. Вот они уже выстукивают ритм. Эта книга заражена бледным струпом, и там, где грибок разъел оболочку моей души, к ней пристал дуэндразон. [Ваш хор постиг недуг; предмет оказался с болезнью.]\"},\n            \"aspects\": {\"rank\":11,\"heart\":1,\"ability\":1,\"malady\":1,\"malady.cure.moth\":5}\n        },\n        {\n            \"id\": \"mere\",\n            \"label\": \"Эреб: вестенгрюр\",\n            \"desc\": \"«Вестенгрюр» – так звала братия Св. Брендана эту хворь. Монахи писали о тоске по зелени, о прелести запустения, об ужасе пустыни. Теперь я их понимаю. О, Лес. О, Лес. О, Лес!..\",\n            \"xtriggers\":{\"malady.curing\":\"zere\",\"contamination.bloodlines\":[{\"id\":\"ability.exposed.bloodlines\",\"morpheffect\":\"mutate\",\"level\":1}],\"contamination.keeperskin\":[{\"id\":\"ability.exposed.keeperskin\",\"morpheffect\":\"mutate\",\"level\":1}]},\n            \"xexts\":{\"malady.inflicting\":\"В минуту уныния скрипучий страх незримо проник в моё сердце, словно незваный гость, вползающий в неплотно затворённое окно. [Ваш эреб постиг недуг.] \",\n            \"contamination.bloodlines\":\"Слова в этой книге сочатся кровью – она заражена гемоглоссами. Бережно промокая страницы, я вдруг ощущаю беспричинный страх сродни страху темноты – не просто чёрной, но сырой и затхлой. [Ваш эреб постиг недуг; предмет оказался заражён паразитом.]\",\n            \"contamination.keeperskin\":\"Поверхность этого предмета покрыта пятнами фосфоресцирующего грибка. Бледный струп. Я вижу его слабое свечение, даже когда закрываю глаза. Что-то пахнущее сыростью, древесное оставило свой отпечаток в моей душе. [Ваш эреб постиг недуг; предмет оказался с болезнью.]\"},\n            \"aspects\": {\"rank\":11,\"nectar\":1,\"ability\":1,\"malady\":1,\"malady.cure.forge\":5}\n        },\n        {\n            \"id\": \"mfet\",\n            \"label\": \"Фет: гистинг\",\n            \"desc\": \"Во снах я был в Доме, что за гранью мира... и теперь, даже когда я проснулся, некая часть меня по-прежнему пребывает там.\",\n            \"xtriggers\":{\"malady.curing\":\"zfet\",\"contamination.winkwell\":[{\"id\":\"ability.exposed.winkwell\",\"morpheffect\":\"mutate\",\"level\":1}],\"contamination.curse.fifth.eye\":[{\"id\":\"ability.exposed.curse.fifth.eye\",\"morpheffect\":\"mutate\",\"level\":1}]},\n            \"xexts\":{\"malady.inflicting\":\"Мысли путаются, и вот уже частица моей души упархивает куда-то, стремясь к далёкому свету – то ли вещественному, то ли кажущемуся. [Ваш фет постиг недуг.]\",\n            \"contamination.winkwell\":\"Глаза. Там. Еле видимые, они лукаво подмигивают мне. Это ненасытные очи. Я всё ещё вижу их, даже сомкнув веки. [Ваш фет постиг недуг; предмет оказался с болезнью.]\",\n            \"contamination.curse.fifth.eye\":\"Проклятие! Сомнений нет: это проклятие пятого глаза, и теперь оно распахнуло некогда сомкнутое око моей души. [Ваш фет постиг недуг; на предмет наложено проклятие.]\"},\n            \"aspects\": {\"rank\":11,\"rose\":1,\"ability\":1,\"malady\":1,\"malady.cure.grail\":5}\n        },\n        {\n            \"id\": \"mhea\",\n            \"label\": \"Здоровье: болезнь\",\n            \"desc\": \"Мне нехорошо.\",\n            \"xtriggers\":{\"malady.curing\":\"zhea\",\"contamination.chionic\":[{\"id\":\"ability.exposed.chionic\",\"morpheffect\":\"mutate\",\"level\":1}],\"contamination.sthenic.taint\":[{\"id\":\"ability.exposed.sthenic.taint\",\"morpheffect\":\"mutate\",\"level\":1}]},\n            \"xexts\":{\"malady.inflicting\":\"Мои телесные силы истощены. [Ваше здоровье ослаблено недугом.]\",\n            \"contamination.chionic\":\"Бледное сияние обожгло холодом мои руки. Теоплазмическая порча. [Ваших членов, как и этого предмета, коснулась порча.]\",\n            \"contamination.sthenic.taint\":\"Боль множеством невидимых уколов пронзает мои руки, словно кто-то утыкал их крошечными иглами. Порча – то ли яд, то ли проклятие, называемое стенической немощью. [Ваше здоровье ослаблено недугом.]\"},\n            \"aspects\": {\"rank\":11,\"winter\":1,\"ability\":1,\"malady\":1,\"malady.cure.heart\":5}\n        },\n        {\n            \"id\": \"mpho\",\n            \"label\": \"Фост: фиксация\",\n            \"desc\": \"Я ВОЗНОШУСЬ и вижу ВСЁ БОЛЬШЕ\",\n            \"xtriggers\":{\"malady.curing\":\"zpho\",\"contamination.actinic\":[{\"id\":\"ability.exposed.actinic\",\"morpheffect\":\"mutate\",\"level\":1}],\"contamination.curse.fifth.eye\":[{\"id\":\"ability.exposed.curse.fifth.eye\",\"morpheffect\":\"mutate\",\"level\":1}]},\n            \"xexts\":{\"malady.inflicting\":\"Огонь моей души мигает – и вдруг ослепительно вспыхивает. Мной завладела опасная фиксация. [Ваш фост постиг недуг.]\",\n            \"contamination.curse.fifth.eye\":\"Проклятие! Сомнений нет: это проклятие пятого глаза, и теперь оно распахнуло некогда сомкнутое око моей души. [Ваш фост постиг недуг.]\",\n            \"contamination.actinic\":\"Предмет окружён сиянием, от которого режет в глазах. Теоплазмическая порча. [Ваш фост постиг недуг; предмет оказался с порчей.]\"},\n            \"aspects\": {\"rank\":11,\"lantern\":1,\"ability\":1,\"malady\":1,\"malady.cure.nectar\":5}\n        },\n        {\n            \"id\": \"msha\",\n            \"label\": \"Шапт: акузия\",\n            \"desc\": \"Двери моей души безжалостно распахнуты. Каждый звук отдаётся колокольным гулом. Каждое слово ранит глаз и кожу...\",\n            \"xtriggers\":{\"malady.curing\":\"zsha\",\"contamination.witchworms\":[{\"id\":\"ability.exposed.witchworms\",\"morpheffect\":\"mutate\",\"level\":1}],\"contamination.sthenic.taint\":[{\"id\":\"ability.exposed.sthenic.taint\",\"morpheffect\":\"mutate\",\"level\":1}]},\n            \"xexts\":{\"malady.inflicting\":\"Двери моей души безжалостно распахнуты. Каждый звук отдаётся колокольным гулом. Каждое слово ранит глаз и кожу...\",\n            \"contamination.sthenic.taint\":\"Боль множеством невидимых уколов пронзает мои руки, словно кто-то утыкал их крошечными иглами. Порча – то ли яд, то ли проклятие, называемое стенической немощью. [Ваш шапт постиг недуг.]\",\n            \"contamination.witchworms\":\"Приникая к странице, я слышу тихий шёпот. Ведьмин червь! [Вы прислушались к нашёптыванию червя, и теперь ваш шапт постиг недуг; предмет оказался заражён паразитом.]\"},\n            \"aspects\": {\"rank\":11,\"knock\":1,\"ability\":1,\"malady\":1,\"malady.cure.winter\":5}\n        },\n        {\n            \"id\": \"mtri\",\n            \"label\": \"Трист: отчаяние\",\n            \"desc\": \"Меланхолия – туман, которым подёргивается гладь души. Отчаяние – волк, рыщущий у её берегов.\",\n            \"xtriggers\":{\"malady.curing\":\"ztri\",\"contamination.actinic\":[{\"id\":\"ability.exposed.actinic\",\"morpheffect\":\"mutate\",\"level\":1}],\"contamination.chionic\":[{\"id\":\"ability.exposed.chionic\",\"morpheffect\":\"mutate\",\"level\":1}]},\n            \"xexts\":{\"malady.inflicting\":\"Чрезмерное душевное напряжение подточило мои силы. [Ваш трист постиг недуг.]\",\n            \"contamination.chionic\":\"Бледное сияние обожгло холодом мои руки. Теоплазмическая порча. [На вашем тристе, как и на этом предмете, теперь лежит порча.]\",\n            \"contamination.actinic\":\"Предмет окружён сиянием, от которого режет в глазах. Теоплазмическая порча. [На вашем тристе, как и на этом предмете, теперь лежит порча.]\"},\n \n            \"aspects\": {\"rank\":11,\"edge\":1,\"ability\":1,\"malady\":1,\"malady.cure.sky\":5}\n        },\n        {\n            \"id\": \"mwis\",\n            \"label\": \"Вист: память хитина\",\n            \"desc\": \"Я путаюсь в незнакомых мыслях. В людях всегда было что-то от Хитинового Креста – тех, что предваряли нас. Теперь оно вернулось.\",\n            \"xtriggers\":{\"malady.curing\":\"zwis\",\"contamination.winkwell\":[{\"id\":\"ability.exposed.winkwell\",\"morpheffect\":\"mutate\",\"level\":1}],\"contamination.witchworms\":[{\"id\":\"ability.exposed.witchworms\",\"morpheffect\":\"mutate\",\"level\":1}]},\n            \"xexts\":{\"malady.inflicting\":\"Мой разум утомлён. В голове мелькают смутные, чужие мысли. [Ваш вист постиг недуг.]\",\n            \"contamination.witchworms\":\"Приникая к странице, я слышу тихий шёпот. Ведьмин червь! [Вы прислушались к нашёптыванию червя, и теперь ваш вист постиг недуг; предмет оказался заражён паразитом.]\",\n            \"contamination.winkwell\":\"Глаза. Там. Еле видимые, они лукаво подмигивают мне. Это ненасытные очи. Я всё ещё вижу их, даже сомкнув веки. [Ваш вист постиг недуг; предмет оказался с болезнью.]\"},\n            \"aspects\": {\"rank\":11,\"scale\":1,\"ability\":1,\"malady\":1,\"malady.cure.lantern\":5}\n        }\n    ]\n}\n";
            Dictionary<string, string> elements = JsonParser.ParseObject(json);
            string[] abilitiesString = JsonParser.ParseArray(elements["elements"]);
            List<Ability> abilities = new(); 
            foreach (string ability in abilitiesString)
            {
                abilities.Add(new Ability(ability));
                Console.WriteLine(new Ability(ability));
            }
        }
    }
}
