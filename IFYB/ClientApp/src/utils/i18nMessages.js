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
        acceptTerms: 'You must accept the terms and conditions',
        badSharingUrl: 'URL must be valid',
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
        fast: 'Fast',
        fastDescription: 'Bugfixing within 3 workdays.',
        fixPrice: 'Fix Price',
        fixPriceDescription: 'Does not matter if it takes half an hour or multiple days.',
        easy: 'Easy submission',
        easyDescription: 'It takes just a few minutes.',
        refundable: 'Fully Refundable',
        refundableDescription: 'Either get a bugfix within 3 workdays, or get your money back.',
      },
      nameCard: {
        name: 'Gabor Angyal',
        description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.',
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
        orderNow: 'Order Now',
        excludeVat: 'The price does not include VAT.'
      },
      handle: {
        title1: 'How It Works',
        subTitle: 'The process of submitting bugs and receiving fixes is quite straightforward. Here is how it works.',
        workflowTitle1: '1. Prepare for submission',
        workflowDescription1: 'To submit a bug, you need two things: a standalone project that is easy to compile and run, and a clear way to reproduce the bug.',
        workflowTitle2: '2. Submit Your Bug',
        workflowDescription2: 'Click on the order button, and answer a few questions about you project, and your bug. Submit your order, and wait for the confirmation, wich usually takes one workday.',
        workflowTitle3: '3. Make the payment',
        workflowDescription3: 'When your order is confirmed, you will be notified by e-mail. Every bugfix is fix priced, you can choose to pay either $&nbsp;{usdPrice} or €&nbsp;{eurPrice}.',
        workflowTitle4: '4. I attempt to fix your bug',
        workflowDescription4: 'After you make your payment, I have { workdays } workdays to fix your bug.',
        workflowTitle5: '5. You are notified when the bug is fixed',
        workflowDescription5: 'If I succeed in fixing your bug, I will send you a pull request, and notify you by e-mail.',
        workflowTitle6: '6. If I can not fix the bug, you get a full refund',
        workflowDescription6: 'If I do not succeed fixing your bug in time, you will automatically receive a full refund.',
        getStarted: 'Get Started'
      },
      footer: {
        title: 'I Fix Your Bug',
        copyright: 'Copyright © 2022 I Fix Your Bug by CodeSharp Kft.',
        pages: 'Pages',
        home: 'Home',
        myOrders: 'My orders',
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
        answer1: 'Please invite me to to your repo, you find my account ',
        link1: 'https://github.com/agabor',
        question2: 'I have a private repository on BitBucket, that I would like to submit a bug for. How do I share the code with you?',
        answer2: 'Please invite me to to your repo, you find my account ',
        link2: 'https://bitbucket.org/agabor',
        question3: 'I have a private repository on GitLab, that I would like to submit a bug for. How do I share the code with you?',
        answer3: 'Please invite me to to your repo, you find my account ',
        link3: 'https://gitlab.com/agabor',
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
        title: 'Contact me',
        subTitle: "For further questions, including partnership opportunities, please email gabor{'@'}ifixyourbug.com or contact using our contact form.",
        fullName: 'Full Name',
        email: 'Email',
        emailPlaceholder: "info{'@'}codesharp.hu",
        howCanWeHelp: 'How can I help you?',
        problemDes: 'Please type ypur message here.',
        sendMessage: 'Send Message'
      },
      contactSuccess: {
        title: 'Thank you!',
        subTitle: 'I will contact you shortly via email.',
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
        canceled: 'Canceled',
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
        emailDes: 'I will email you a magic code for a password-free sign in.',
        submit: 'Submit',
        name: 'Name',
        nameDes: 'Enter your name.',
        save: 'Save',
        orderData: 'Order data',
        orderDataDes: 'Enter data from your app.',
        emailExample: "email{'@'}example.com",
        successfulOrder: 'Successful order',
        successfulOrderDes: 'I will contact you shortly via email.',
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
      canceled: 'Canceled',
      name: 'Name',
      email: 'Email',
    },
    //OrderMessages
    orderMessages: {
      title: 'Messages',
      newMessagePlaceholder: 'Aa'
    },
    //2FA
      authentication: {
        title: 'Check your email for a code',
        subTitle: 'I have sent a 6-character code to <b>{email}</b>. The code expires shortly, so please enter it soon.',
        buttonText: 'Check',
        cancel: 'Cancel'
      },
      authfailed: {
        title: 'Authentication Failed',
        subTitle: 'You have entered wrong code 3 times.',
        buttonText: 'Go Back'
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
        payDescription: 'Please choose the currency you would like to pay.',
        pay: 'Pay',
        loading: 'loading',
        checkoutLink: 'Checkout link expired.',
        backToHome: 'Bact to home',
        notfound: 'Order not found',
        back: 'Back'
      },
    //CookieModal
      cookie: {
        title: "Cookies!",
        subtitle: "I use cookies to make your experience better",
        analytics: "Analytics",
        advertisement: "Advertisement",
        accept: "Accept",
        customise: "Customise",
        acceptAll: "Accept all",
        rejectAll: "Reject all",
        save: "Save",
      },
      //TimeoutModal
      timeout: {
          title: "Your login session timed out.",
          subtitle: "Please login to gain access the website's functions",
          later: "Later",
          login: "Login",
        }
    }
  }