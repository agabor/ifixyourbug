export const messages = {
    en: {
    //Errors
      errors: {
        requiredFramework: 'Framework required',
        requiredVersion: 'Version required',
        requiredOS: 'Operating system required',
        requiredOSVersion: 'Operating system version required',
        requiredBrowserType: 'Browser type required',
        requiredBrowserVersion: 'Browser version required',
        requiredAppUrl: 'App url required',
        requiredGitRepoUrl: 'Git repo url required',
        requiredProjectSharing: 'Project sharing required',
        requiredBugDes: 'Bug description required',
        requiredThirdPartyTool: 'Third party tool required',
        requiredName: 'Name required',
        requiredEmail: 'Email required',
        requiredValidEmail: 'Valid email required.',
        requiredMessage: 'Message required',
        requiredNewMessage: 'Message required',
        notAdministratorEmail: 'This email is not an administrator email.',
        minLength: 'Minimum character is ',
        somethingWrong: 'Something wrong',
        wrongCode: 'Wrong code.',
        noResult: 'No result',
        acceptTerms: 'You must accept the terms and conditions'
      },
    //Policies
      policies: {
        iAcceptAndRead: 'I have read and accept',
        privacyPolicy: 'the privacy policy',
        requiredPrivacyPolicy: 'Approval of the privacy policy is required',
        termsAndConditions: 'the terms and conditions',
      },
    //SearchBar.vue
      searchBar: {
        search: 'Search...'
      },
    //NavigationBar
      navigationBar: {
        projectName: 'I Fix Your Bug',
        home: 'Home',
        myOrders: 'My orders',
        services: 'Services',
        faq: 'FAQ',
        orders: 'Orders',
        clients: 'Clients',
        login: 'Login',
        logout: 'Logout',
      },
    //HomeView
      mainCard: {
        title: 'I Fix Your Bug',
        description: 'ASP.NET Core and Vue.js bugfixing service. For when you need a second pair of eyes.',
        pricing: 'Pricing',
        contact: 'Contact',
        asp: 'ASP.NET Core',
        vuejs: 'Vue.js',
      },
      features: {
        components: 'Components',
        componentsDes: 'Choose the best design system for your next product.',
        design: 'Design',
        designDes: 'Get the latest design ideas and turn it into reality.',
        lessCode: 'Less Code',
        lessCodeDes: 'Make your code easier to maintain using variables.',
        fullyResponsive: 'Fully Responsive',
        fullyResponsiveDes: 'This design system is fully supported on any device.',
      },
      nameCard: {
        name: 'Michael Roven',
        follow: 'Follow',
        followers: 'Followers',
        following: 'Following',
        posts: 'Posts',
        description: 'Decisions: If you can’t decide, the answer is no. If two equally difficult paths, choose the one more painful in the short term (pain avoidance is creating an illusion of equality). Choose the path that leaves you more equanimous.',
        more: 'More about me',
      },
      workWithUs: {
        title: 'Technologies',
        subTitle: 'More than full-stack',
        carouselDes1: 'No software exists in isolation, and some bugs are beyond the strict scope of the application. To find the trickiest bugs you might need someone, who has experience with a variety of web servers, cloud providers and deployment environments.',
      },
      pricing: {
        mainTitle: 'Best no-tricks pricing',
        mainSubTitle: 'Fix price for all bugs.',
        title: 'Single Bugfix',
        description: 'ASP.NET Core and Vue.js bugfixing within {workdays} workdays',
        whatsincluded: 'What’s included',
        include1: 'ASP.NET Core',
        include2: 'Vue.js',
        include3: 'Bugfix delivered as pull request',
        include4: 'Explaination of the bugfix',
        payOne: 'One time payment, no hidden costs',
        orderNow: 'Order Now'
      },
      handle: {
        title1: 'How It Works',
        subTitle: 'The process of submitting bugs and receiving fixes is quite straightforward. Here is how it works.',
        searchAndDiscover: 'Search and Discover!',
        websiteVisitors: 'Website visitors',
        getStarted: 'Get Started',
        workflowTitle1: '1. Prepare for submission',
        workflowDescription1: 'To submit a bug, you need two things: a standalone project that is easy to compile and run, and a clear way to reproduce the bug.',
        workflowTitle2: '2. Submit Your Bug',
        workflowDescription2: 'Click on the order button, and answer a few questions about you project, and your bug. I will review your submissionm and decide if this is something I can fix or not.',
        workflowTitle3: '3. Make the payment',
        workflowDescription3: 'If I accept your submission, you will be notified by e-mail. You will receive a payment link. My service has a fix price ({price} USD or EUR), not mather what kind of bug you submit. You can choose your prefered currency on the checkout page.',
        workflowTitle4: '4. I attempt to fix your bug',
        workflowDescription4: 'I will do my best, I promise.',
        workflowTitle5: '5. You are notified when the bug is fixed',
        workflowDescription5: 'If I succeed in fixing your bug, I will make you a pull request.',
        workflowTitle6: '6. If I can not fix the bug, you get a full refund',
        workflowDescription6: 'After you make your payment, I have 3 workdays to fix your bug. If I do not succeed in that time, you are eligible for a full refund.',
        talAndMeet: 'Talk and Meet!',
        socialActivities: 'Social activities',
      },
      footer: {
        title: 'I Fix Your Bug',
        copyright: 'Copyright © 2022 I Fix Your Bug by CodeSharp Kft.',
        pages: 'Pages',
        home: 'Home',
        myOrders: 'My orders',
        services: 'Services',
        design: 'Design',
        faq: 'FAQ',
        reader: 'Reader',
        terms: 'Terms & conditions',
        privacyPolicy: 'Privacy policy',
      },
    //FAQ
      faqCard: {
        title: 'FAQ',
        lastModified: 'Last modified: {date}',
      },
      faqSecurity: {
        title: 'Security',
        question1: 'Is my code safe with you?',
        answer1: 'When you submit your order, you provide access to your git repository. When your order is competed, you are free to revoke my access. I will delete my local copy of your repository when your order is completed.',
        question2: 'Can you sign an NDA?',
        answer2: "Yes, please send an e-mail to gabor{'@'}ifixyourbug.com to discuss the details.",
      },
      faqRepos: {
        title: 'Repositories',
        question1: 'I have a private repository on GitHub, that I would like to submit a bug for. How do I share the code with you?',
        answer1: 'Please invite me to to your repo, you find my account <a href="https://github.com/agabor" target="_blank">here.</a>',
        question2: 'I have a private repository on BitBucket, that I would like to submit a bug for. How do I share the code with you?',
        answer2: 'Please invite me to to your repo, you find my account <a href="https://bitbucket.org/agabor" target="_blank">here.</a>',
        question3: 'I have a private repository on GitLab, that I would like to submit a bug for. How do I share the code with you?',
        answer3: 'Please invite me to to your repo, you find my account <a href="https://gitlab.com/agabor" target="_blank">here.</a>',
        question4: 'I have a self hosted repository, that I would like to submit a bug for. How do I share the code with you?',
        answer4: "You can either invite me by e-mail, or manually create an account for me. If you would like to invite me, please send the invitation to gabor{'@'}ifixyourbug.com. If you would like co create an account for me, than please share the credentials in your bug description. If you use SSH keys on your server, please add the following public key to my account:",
      },
      faqOrders: {
        title: 'Orders',
        question1: 'How long does it take to process my order?',
        answer1: 'Within 1 workday. I will checkout your code, and see if the bug you described is something that I can fix or not. After that, you will get an e-mail that informs you whether your order is accepted or not.',
        question2: 'What are the requirements for an order to be accepted?',
        answer2: 'First of all, it should be clear how to run your project. If your project needs some kind of custom configuration to start, please provide a description. The bug description must be clear, and the bug must be reproduceable.',
        question3: 'How long does it take to receive a bugfix?',
        answer3: 'If your order is accepted, you will receive a payment link by e-mail. After you make a payment, I have 3 workdays to fix your bug. If you do not receive a working bugfix within 3 workdays, you will be fully refunded.',
      },
      faqRefunds: {
        title: 'Refunds',
        question1: 'How do I get a refund?',
        answer1: "If I can not deliver a working bugfix within 3 workdays, you do not have to do anything, you will autiomatically get a full refund. If you receive a bugfix, but you are not satisfied, use the messageing feature on you order page, or send an e-mail to gabor{'@'}ifixyourbug.com, and tell me about your problem. You will be refunded for any reasonable claim.",
      },
    //DesignView
      design: {
        title: 'Designed by',
        lastModified: 'Last modified: {date}',
        images: 'Images',
        webDesign: 'Web Design',
      },
    //ContactForm
      contact: {
        title: 'Contact us',
        subTitle: "For further questions, including partnership opportunities, please email gabor{'@'}ifixyourbug.com or contact using our contact form.",
        fullName: 'Full Name',
        email: 'Email',
        emailPlaceholder: "info{'@'}codesharp.hu",
        howCanWeHelp: 'How can we help you?',
        problemDes: 'Describe your problem in at least 250 characters',
        sendMessage: 'Send Message',
        createAccount: 'You are not currently logged in. If you send the message, we will create your account.'
      },
      contactSuccess: {
        title: 'Thank you!',
        subTitle: 'We will contact you shortly via email.',
        backToHome: 'Bact to home',
      },
    //ContactMessages
      contactMessages: {
        select: 'Select one client',
        haveNoMessages: 'Have no messages'
      },
    //OtherServices
      otherServices: {
        pricing: 'Pricing',
        title: 'See our pricing',
        subTitle: 'You have Free Unlimited Updates and Premium Support on each package.',
      },
      otherCard1: {
        title: 'Consultation',
        price: '199',
        element1: '1',
        element2: '2',
        element3: '3',
        element4: '4',
        button: 'Buy now'
      },
      otherCard2: {
        title: 'Bug fixing',
        price: '299',
        element1: '1',
        element2: '2',
        element3: '3',
        element4: '4',
        element5: '5',
        button: 'Try pro'
      },
      otherCard3: {
        title: 'Code Review',
        price: '399',
        element1: '1',
        element2: '2',
        element3: '3',
        element4: '4',
        button: 'Buy now'
      },
    //OrderList
      orderList: {
        number: 'Number',
        name: 'Name',
        email: 'Email',
        framework: 'Framework',
        version: 'Version',
        thirdPartyTool: 'Third party tool',
        applicationUrl: 'Application url',
        specificPlatform: 'Specific platform',
        pay: 'Pay',
        state: 'State',
        submitted: 'Submitted',
        accepted: 'Accepted',
        rejected: 'Rejected',
        payed: 'Payed',
        completed: 'Completed',
        refundable: 'Refundable',
        addNewOrder: 'Add new order',
        details: 'Details'
      },
    //ClientList
      clientList: {
        name: 'Name',
        email: 'Email',
        messages: 'Messages',
      },
    //NewOrderView
      order: {
        email: 'Email',
        emailDes: 'Enter your email.',
        submit: 'Submit',
        name: 'Name',
        nameDes: 'Enter your name.',
        save: 'Save',
        orderData: 'Order data',
        orderDataDes: 'Enter data from your app.',
        emailExample: "email{'@'}example.com",
        successfulOrder: 'Successful order',
        successfulOrderDes: 'We will contact you shortly via email.',
        backToHome: 'Bact to home',
      },
    //NewOrderForm
      newOrder: {
        submit: 'Submit',
        cancel: 'Cancel',
        bugDescription: 'Bug description',
      },
    //OrderViewer
    orderViewer: {
      title: 'Order',
      back: 'Back to list',
      accept: 'Accept',
      reject: 'Reject',
      rejectWithMessage: 'Reject with message',
      pay: 'Pay',
      submitted: 'Submitted',
      accepted: 'Accepted',
      rejected: 'Rejected',
      payed: 'Payed',
      completed: 'Completed',
      refundable: 'Refundable',
      name: 'Name',
      email: 'Email',
    },
    //OrderMessages
    orderMessages: {
      title: 'Messages',
      newMessagePlaceholder: 'Aa'
    },
    //2FA
      twofa: {
        title: '2FA Security',
        subTitle: 'Enter 6-digits code from your athenticatior app.',
        buttonText: 'Check',
      },
    //SelectFramework
      framework: {
        label: 'Framework*',
        placeholder: 'Select a framework',
        option1: 'Vue.js',
        option2: 'ASP.NET Core',
      },
    //SelectVersion
      frameworkVersion: {
        label: 'Version*',
        placeholder: 'Select version',
        frameworkFirst: 'Please select a framework first',
      },
    //OperatingSystem
      operatingSystem: {
        isSpecific: 'Is the issue specific to an operating system?',
        label: 'Operating system*',
        option1: 'Windows',
        option2: 'Linux',
        option3: 'MacOS',
        version: 'Operating system version',
      },
    //BrowserType
      browserType: {
        isSpecific: 'Is the issue specific to an browser?',
        label: 'Browser type*',
        option1: 'Chrome',
        option2: 'Safari',
        option3: 'Firefox',
        version: 'Browser version',
      },
    //OnlineApp
      onlineApp: {
        isAvailable: 'Is there a deployed version of the application available online?',
        appUrl: 'Application url',
      },
    //GitAccessSelector
      gitAccessSelector: {
        label: 'Previous accesses',
        placeholder: 'Select a previous access',
      },
    //ProjectSharing
      projectSharing: {
        urlLabel: 'Git repo url*',
        urlPlaceholder: 'https://..',
        sharingLabel: 'Project sharing with*',
        option1: 'Public repository',
        option2: 'Invite to repository',
        option3: 'Create account for repository',
        description1: 'Use this option if your project is hosted publicly. (For example on GitHub, GitLab or Bitbucket.)',
        description2: "Use this option if your project is hosted privately, and you wish to invite me. If your project is hosted on GitHub, GitLab or Bitbucket, please invite the following user: agabor (gabor{'@'}ifixyourbug.com). If your project is hosted elsewhere, please send an invitation link to this e-mail address.",
        description3: 'Use this option if your project is hosted privately, and you wish to create an account for me. Please include the user credentials in the bug description. If you want to use an SSH key, please use the following public key:',
      },
    //ThirdPartyTool
      thirdPartyTool: {
        isPotentially: 'Is the bug potentially related to a third party library?',
        label: 'Third party library name',
      },
    //ConfirmationModal
      confirm: {
        stateChangeTitle: 'Change of state',
        stateChangeDescription: 'Do you really want to change the status of the state?',
        message: 'Message to the user',
        cancel: 'Cancel',
        confirm: 'Yes, got it!',
      },
    //CheckoutViews
      checkout: {
        successTitle: 'Payment Successful!',
        successSubTitle: 'Thank you for choosing us.',
        failedTitle: 'Payment Failed!',
        failedSubTitle: 'Payment failed, please try again.',
        paidTitle: 'Already Paid!',
        paidSubTitle: 'Thank you for choosing us.',
        order: 'Order',
        payDescription: 'Please pay so we can start work as soon as possible.',
        pay: 'Pay',
        loading: 'loading ...',
        checkoutLink: 'Checkout link expired.',
        backToHome: 'Bact to home',
        notfound: 'Order not found',
        back: 'Back'
      },
    //CookieModal
      cookie: {
        title: "Cookies!",
        subtitle: "We use cookies to make your experience better",
        analytics: "Analytics",
        advertisement: "Advertisement",
        accept: "Accept",
        customise: "Customise",
        acceptAll: "Accept all",
        rejectAll: "Reject all",
        save: "Save",
      }
    }
  }