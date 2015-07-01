#import "IOSHelper.h"

@implementation IOSHelper{
    
}

- (instancetype)init
{
    self = [super init];
    return self;
}


@end

// Converts C style string to NSString
NSString* CreateNSString (const char* string)
{
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}

// Helper method to create C string copy
char* MakeStringCopy (const char* string)
{
	if (string == NULL)
		return NULL;
	
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy(res, string);
	return res;
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
    
    bool _CanOpenURL(const char* url) {
        bool success = false;
        success = [[UIApplication sharedApplication] canOpenURL:[NSURL URLWithString:CreateNSString(url)]];
        return success;
    }
    
    
}