<p>Imagine that you are working on an existing application that sends notifications to other programs. The initial version of that application was only sending notifications by email but now you are asked to add some additional features in that library so that it can start sending notifications by SMS, or can send notifications to Facebook, Twitter, etc., or a combination of many other apps. You don&rsquo;t want to modify existing code, you don&rsquo;t want to create a big hierarchy of child and grandchild classes and still, you want to enhance the existing application. This is where the Decorator Pattern will come to rescue you and will allow you to dynamically add or remove functionality to existing classes.</p>

<p><strong>Let&#39;s get started</strong></p>

<p><strong>1.</strong><strong>What is a Decorator Pattern?</strong></p>

<p>The decorator pattern (also known as Wrapper) is a structural design pattern and it allows developers to dynamically add new behaviors and features to existing classes without modifying them thus respecting the&nbsp;<a href="https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle" rel="noreferrer noopener" target="_blank">open-closed principle</a>.&nbsp; This pattern lets you structure your business logic into layers (wrappers) in a way that each layer adds some additional behavior or functionality to an existing object, promoting&nbsp;<a href="https://en.wikipedia.org/wiki/Separation_of_concerns" rel="noreferrer noopener" target="_blank">separation of concern</a>. Furthermore, these layers can be added or removed at runtime and clients can also use the different combinations of decorators to be attached to an existing object.</p>

<p><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/Decorator.PNG?raw=true" style="height:519px; width:685px" /></p>

<p><strong>2.&nbsp;</strong><strong>Pros Decorator Pattern</strong></p>

<ol>
	<li>We can extend an object&rsquo;s behavior without creating a hierarchy of new child classes.</li>
	<li>We can add or remove features from an object at runtime which gives developer flexibility not available in simple inheritance.</li>
	<li>We can combine several features by wrapping an object into multiple decorators</li>
	<li>We can divide a complex object into several smaller classes with specific behaviors which promotes the Single Responsibility Principle</li>
	<li>It supports the Open-closed principle which states that the classes should be open for extension but closed for modification.</li>
</ol>

<p><strong>3.&nbsp;Cons Decorator Pattern</strong></p>

<ol>
	<li>The object instantiation can be complex as we have to create an object by wrapping it in several decorators.</li>
	<li>Sometimes, it&rsquo;s hard to keep track of the full wrapper stack, and removing a specific wrapper from the stack is not something easy to achieve.</li>
	<li>Decorators can cause issues if the client using them relies heavily on the object concrete type.</li>
</ol>

<p><strong>Getting Started with Decorator Pattern in ASP.NET Core 5</strong></p>

<p>The decorator pattern can be used to attach cross-cutting concerns such as logging or caching to existing classes without changing their code. Let&rsquo;s create a new ASP.NET Core 5 REST API to learn how to use the decorator pattern to dynamically add/remove logging and caching features.</p>

<p>First of all, create the following&nbsp;<strong>Player</strong>&nbsp;model class in the&nbsp;<strong>Models</strong>&nbsp;folder of the project.</p>

<p><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/Player.PNG?raw=true" style="height:142px; width:814px" /></p>

<p>Next, create the following&nbsp;<strong>PlayerService</strong>&nbsp;and return a fake list of players. Of course, in a live application, this type of service will fetch data from a backend database but I want to keep the example simple as the purpose of this post is to show you the usage of decorator patterns in real-world scenarios.</p>

<p><strong>IPlayerService.cs</strong></p>

<p><strong><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/IPlayerService.PNG?raw=true" /></strong></p>

<p><strong>PlayerService.cs</strong></p>

<p><strong><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/PlayerService.PNG?raw=true" /></strong></p>

<p>Inject the above&nbsp;<strong>PlayerService</strong>&nbsp;in an ASP.NET Core API Controller and call the&nbsp;<strong>GetPlayersList</strong>&nbsp;method as shown below.</p>

<p><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/Controller.PNG?raw=true" /></p>

<p>Run the project and make a get request to fetch the list of players. You should see the players list on the page as shown below.</p>

<p><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/APIResponse.PNG?raw=true" /></p>

<p>Everything is pretty straightforward so far as we are using standard services and controllers in ASP.NET Core.</p>

<h2><strong>Implementing a Logging Decorator</strong></h2>

<p>The first decorator I want to attach with the above&nbsp;<strong>PlayerService</strong>&nbsp;is a logging decorator. This decorator will allow our service to output the log messages at runtime. This can be very useful in a production environment where you want to see how your services are working internally by logging messages to different sources. Let&rsquo;s create a class&nbsp;<strong>PlayerServiceLoggingDecorator</strong>&nbsp;and implement the same&nbsp;<strong>IPlayerService</strong>&nbsp;interface on it.</p>

<p><strong>PlayersServiceLoggingDecorator.cs</strong></p>

<p><strong><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/PlayersServiceLoggingDecorator.PNG?raw=true" /></strong></p>

<p>We are injecting the instances of&nbsp;<strong>IPlayerSerice</strong>&nbsp;and&nbsp;<strong>ILogger</strong>&nbsp;in the decorator constructor using the dependency injection. The logging decorator is implementing the&nbsp;<strong>IPlayersService</strong>&nbsp;interface so it has to define the&nbsp;<strong>GetPlayersList</strong>&nbsp;method. Inside the&nbsp;<strong>GetPlayersList</strong>&nbsp;method, we are calling the&nbsp;<strong>GetPlayersList</strong>&nbsp;method implemented by&nbsp;<strong>PlayerService</strong>&nbsp;and once we have the players list available, we are simply iterating over them to log their Id and Name. &nbsp;There are also few other&nbsp;<strong>LogInformation</strong>&nbsp;method calls to log different types of messages. We are also using the&nbsp;<strong>Stopwatch</strong>&nbsp;object to log our method execution time.</p>

<h2><strong>Implementing a Caching Decorator</strong></h2>

<p>The second decorator I want to attach is a caching decorator. This decorator will allow our service to cache the player&rsquo;s list for a certain amount of time so that we don&rsquo;t need to fetch the data from the backend service or database again. This can be useful in applications where you want to improve your application performance. Let&rsquo;s create a class&nbsp;<strong>PlayersServiceCachingDecorator</strong>&nbsp;and implement the same&nbsp;<strong>IPlayerService</strong>&nbsp;interface on it.</p>

<p><strong>PlayersServiceCachingDecorator.cs</strong></p>

<p><strong><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/PlayersServiceCachingDecorator.PNG?raw=true" /></strong></p>

<p>This time, we are injecting the instances of&nbsp;<strong>IPlayerSerice</strong>&nbsp;and&nbsp;<strong>IMemoryCache</strong>&nbsp;in the decorator constructor. Inside the&nbsp;<strong>GetPlayersList</strong>&nbsp;method, we are first checking if the player&rsquo;s list with a matching cache key is available in the memory cache and returning the same list from the cache. If we don&rsquo;t have the player&rsquo;s list in the cache, we are calling the&nbsp;<strong>GetPlayersList</strong>&nbsp;method of&nbsp;<strong>PlayerService</strong>&nbsp;class to get the list and then adding it to the memory cache for one minute.</p>

<h2><strong>Manually Registering the Decorators with DI Container</strong></h2>

<p>We are now ready to register our service and decorators so that they can be injected using the .NET Core dependency injection framework. This is where you will also see how we are wrapping one decorator into another to attach a chain of decorators to an existing service.</p>

<p><strong>Startup.cs</strong></p>

<p><strong><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/ManuallyRegistering.PNG?raw=true" /></strong></p>

<p>We first registered the&nbsp;<strong>PlayerService</strong>&nbsp;using the&nbsp;<strong>AddScoped</strong>&nbsp;method. Then we requested the instance of&nbsp;<strong>PlayerService</strong>&nbsp;using the&nbsp;<strong>GetRequiredService</strong>&nbsp;method to pass it into the constructor of our&nbsp;<strong>PlayersServiceCachingDecorator</strong>&nbsp;class. Finally, the instance of caching decorator is passed in the&nbsp;<strong>PlayersServiceLoggingDecorator</strong>&nbsp;constructor.</p>

<p>With everything in place, let&rsquo;s run the application once again and this time check what messages are logged in the output window and how much time our methods took to execute.</p>

<p><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/ConsoleOutput.PNG?raw=true" /></p>

<p>You can see all the log messages in the output window as shown in the above screenshot. The first time, our memory cache was empty that&rsquo;s why the method took 28 milliseconds to execute. Refresh the player&rsquo;s list page again and this time you will see that method will take less time as compared with the previous request because now the data will be fetched from the memory cache.</p>

<h2><strong>Registering the Decorators using Scrutor library</strong></h2>

<p>We now have a real-world example of using a decorator pattern in an ASP.NET Core application but some of you may not like the way we manually registered our decorators with dependency injection in&nbsp;<strong>Startup. cs</strong>&nbsp;file. We are instantiating the decorators ourselves and passing them to other decorators by calling their constructors. What if the decorator class has many more services injected into the constructor? You don&rsquo;t want to instantiate a big list of services just to pass in the decorator constructor. We want an easy way to register our decorators and this is where&nbsp;<a href="https://github.com/khellang/Scrutor" rel="noreferrer noopener" target="_blank">Scrutor</a>&nbsp;library comes to the rescue.</p>

<p>The&nbsp;<a href="https://github.com/khellang/Scrutor" rel="noreferrer noopener" target="_blank">Scrutor</a>&nbsp;is a small library that includes some extension methods for registering decorators. The simplest and the most common method is&nbsp;<strong>Decorate</strong>&nbsp;which allows us to register decorators just like we register normal classes in .NET. We can install the&nbsp;<a href="https://www.nuget.org/packages/Scrutor" rel="noreferrer noopener" target="_blank">Scrutor</a>&nbsp;library using the&nbsp;<a href="https://www.nuget.org/packages/Scrutor" rel="noreferrer noopener" target="_blank">NuGet Package Manager</a>.</p>

<p>With the help of the&nbsp;<a href="https://github.com/khellang/Scrutor" rel="noreferrer noopener" target="_blank">Scrutor</a>&nbsp;library, the registration of our decorators can be as simple as the following code snippet.</p>

<p><strong>Startup.cs</strong></p>

<p><strong><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/DecoratorWithScrutor.PNG?raw=true" /></strong></p>

<p>Now&nbsp;You will run the application it behaves as expected it was working previously.</p>

<h2><strong>Dynamically Add or Remove Decorators at Runtime</strong></h2>

<p>In a real-world application, you may want to add or remove decorators dynamically at runtime based on different use cases such as:</p>

<ol>
	<li>You may want to add a logging decorator only in the production environment but don&rsquo;t want to log anything in the development environment.</li>
	<li>You may want to use some configuration settings to dynamically add/remove decorators in any environment.&nbsp;</li>
</ol>

<p>We can add&nbsp;<strong>EnableCaching</strong>&nbsp;and&nbsp;<strong>EnableLogging</strong>&nbsp;settings in the&nbsp;<strong>appsettings.json</strong>&nbsp;file and caching and logging can be enabled/disabled using these settings.</p>

<p><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/Dynamic%20Add%20or%20remove%20decorators.PNG?raw=true" /></p>

<p>Here is the code to register decorators based on the above configuration settings.</p>

<p><strong>Startup.cs</strong></p>

<p><img alt="" src="https://github.com/ankit68543/decorator-pattern/blob/master/Images/DynamicallySourceCode.PNG?raw=true" /></p>

<h2>&nbsp;</h2>

<h2><strong>Summary</strong></h2>

<p>The decorator pattern can be used to extend classes or to add cross-cutting concerns without changing their code. In this post, we learned how to use the decorator pattern to add features such as logging and caching in ASP.NET Core APIs . We also learn how to register decorators manually and using the&nbsp;<a href="https://www.nuget.org/packages/Scrutor">Scrutor</a>&nbsp;library. In the end, we learned how to dynamically enable/disable decorators based on configuration settings. I hope, you will keep the decorator pattern in mind for certain use cases while developing your applications.</p>

<p>#Happy #Learning</p>
